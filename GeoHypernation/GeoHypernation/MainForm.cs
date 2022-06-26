using BLL.Services;
using Models.DataControlModels;
using Models.DataViewModels;
using Standard.Licensing;
using Standard.Licensing.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoHypernation
{
    public partial class MainForm : Form
    {
        //readonly string exeDir = Path.GetDirectoryName((new System.Uri(Assembly.GetEntryAssembly().CodeBase)).AbsolutePath);

        private string FileName = "";
        private int DocPageCount = 0;
        private bool Working = false;
        private bool IsSaved = true;

        private string WorkingDir;
        private string WorkingDocPath;
        private int CurrentPage = 0;
        List<int> Pages = null;
        private string RecommendedDocType = ".docx";

        private bool limited = false;

        WorkWithDoc wwd;
        LicenseManagement lc;

        public MainForm()
        {
            InitializeComponent();
            this.AllowDrop = true;
            upload_btn.Click += upload_btn_Click;
            manual_btn.Click += manual_btn_Click;
            this.DragEnter += new DragEventHandler(upload_btn_DragEnter);
            this.DragDrop += new DragEventHandler(upload_btn_DragDrop);
            Change_SaveBtn_State(enabled: false);
            lc = new LicenseManagement();


            //string passPhrase = "ChamshvebiLicense2022";

            //var keyGenerator = Standard.Licensing.Security.Cryptography.KeyGenerator.Create();
            //var keyPair = keyGenerator.GenerateKeyPair();
            //var privateKey = keyPair.ToEncryptedPrivateKeyString(passPhrase);
            //var publicKey = keyPair.ToPublicKeyString();
            //var license = Standard.Licensing.License.New()
            //    .WithUniqueIdentifier(Guid.NewGuid())
            //    .As(LicenseType.Trial)
            //    .ExpiresAt(DateTime.Now.AddDays(45))
            //    .WithMaximumUtilization(5)
            //    .WithProductFeatures(new Dictionary<string, string>
            //        {
            //            {"Limited", "false"}
            //        })
            //    .LicensedTo("GAU", "gau.edu.ge")
            //    .CreateAndSignWithPrivateKey(privateKey, passPhrase);

            //using (var xmlWriter = System.Xml.XmlWriter.Create("License.lic")) { license.Save(xmlWriter); }
            //File.WriteAllText("License.lic", license.ToString(), Encoding.UTF8);
        }

        #region Document Methods
        private void Upload_Doc(string file)
        {
            try
            {
                Upload_panel.SetVisibleAsync(false);
                Change_working_state(true);
                wwd = new WorkWithDoc(file);
                Change_working_state(false);
                WorkingDocPath = (string)wwd.DocPath;
                InitializeDocBox(file);
                this.SetFormBorderStyle(FormBorderStyle.Sizable);
                this.SetMaximizeBox(true);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"მოხდა შეცდომა. ბოდიშს გიხდით \n\n Err_UpDoc", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        //Calling this method when document has changed.
        private void InitializeDocBox(string filename)
        {
            CurrentPage = 1;
            double size = new FileInfo(filename).Length / 1024.0;
            if(size > 1024.0)
                size_lbl.SetTextAsync(String.Format("{0:F2}", size / 1024.0) + " MB");
            else
                size_lbl.SetTextAsync(String.Format("{0:F2}", size) + " Kb");
            type_lbl.SetTextAsync(Path.GetExtension(filename).Replace(".", string.Empty));
            filename_lbl.SetText(Path.GetFileNameWithoutExtension(new FileInfo(filename).Name));

            Graphics g = this.CreateGraphics();
            Font f = filename_lbl.Font;
            string labelstr = filename_lbl.Text;
            char[] char_arr = labelstr.ToCharArray();
            int width;
            int count = 0;
            for (int i = 0; i < char_arr.Length; i++)
            {
                width = (int)(g.MeasureString(labelstr.Substring(labelstr.Length-i, i), f).Width);
                if (width >= filename_lbl.GetWidth())
                {
                    count = i;
                    break;
                }
            }
            if(count > 0)
                filename_lbl.SetText($"...{labelstr.Substring(filename_lbl.Text.Length - (count-2), count-2)}");
                        
            GetPages(true);
        }
        private void GetPages(bool clean = false)
        {
            CurrentPage = (CurrentPage == 0) ? 1 : CurrentPage;

            if (!string.IsNullOrEmpty(FileName) && (Pages == null || !Pages.Intersect(new List<int> { CurrentPage + 1, CurrentPage + 2 }).Any() || (CurrentPage == Pages[0]) || (Pages.Count > 5 && CurrentPage == Pages[4]) || (CurrentPage==1 && !Pages.Contains(1))))
            {
                Thread thread = new Thread
                (delegate ()
                {
                    try
                    {
                        Change_working_state(true);
                        DocDTO result = wwd.GetPages(CurrentPage, clean);
                        WorkingDir = result.TempDirectory;
                        DocPageCount = result.PageCount;
                        Pages = result.Pages;

                        string FilePath = Path.Combine(WorkingDir, $"{CurrentPage}.html");
                        if (File.Exists(FilePath) && Pages.Count > 0)
                        {
                            Navigate_WebBrowser(FilePath);

                            Create_Pagination_Buttons(Pages, DocPageCount);
                        }
                        Change_working_state(false);
                        if (this.Controls.Find($"{CurrentPage}_btn", true).Length > 0)
                            this.Controls.Find($"{CurrentPage}_btn", true).FirstOrDefault().SetEnabled(false);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("მოხდა შეცდომა. ბოდიშს გიხდით. \n\n Err_GetPage", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
        }
        private void Close_Doc()
        {
            try
            {
                Upload_panel.SetVisible(true);
                if (!string.IsNullOrEmpty(FileName))
                {
                    FileName = string.Empty;
                    if (CurrentPage != 0)
                    {
                        wwd.CloseDoc();
                        if (Directory.Exists(WorkingDir))
                            Directory.Delete(WorkingDir, true);

                        if (File.Exists(WorkingDocPath))
                            File.Delete(WorkingDocPath);

                        Navigate_WebBrowser(FileName);
                        Clear_Pagination();
                        CurrentPage = 0;
                        IsSaved = true;
                        Change_SaveBtn_State(enabled: false);
                        //Form Size
                        if (this.GetWindowState() == FormWindowState.Maximized)
                            this.SetWindowState(FormWindowState.Normal);

                        this.SetVisible(false);
                        SuspendLayout();
                        this.SetSize(new Size(1129, 506));
                        this.SetFormBorderStyleAsync(FormBorderStyle.FixedSingle);
                        this.SetMaximizeBoxAsync(false);
                        ResumeLayout();
                        this.SetVisible(true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"მოხდა შეცდომა. ბოდიშს გიხდით \n\n Err_CloseApp", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        #endregion


        #region File Upload Methods
        private void upload_btn_Click(object sender, EventArgs e)
        {
            if (!Working && FileName == string.Empty)
            {
                try
                {
                    OpenFileDialog ofd = new OpenFileDialog()
                    {
                        InitialDirectory = @"Documents",
                        Filter = "Word Document (*.docx)|*.docx|Word Document (*.doc)|*.doc|Rich Text Format (*.rtf)|*.rtf",
                        Title = "გახსენით MS Word დოკუმენტი"
                    };
                    DialogResult result = ofd.ShowDialog(); // Show the dialog.
                    if (result == DialogResult.OK) // Test result.
                    {
                        FileName = ofd.FileName;
                        Upload_Doc(FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("მოხდა შეცდომა. ბოდიშს გიხდით", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void upload_btn_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
        private void upload_btn_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (!Working && FileName == string.Empty)
                {
                    var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (files.Length == 1)
                    {
                        FileName = files.SingleOrDefault();
                        string ext = Path.GetExtension(FileName);
                        if (ext.Equals(".doc") || ext.Equals(".docx") || ext.Equals(".rtf"))
                            Upload_Doc(FileName);
                        else
                        {
                            FileName = string.Empty;
                            MessageBox.Show("ფაილი უნდა იყოს MS Word-ის ტიპის.\n\nდაშვებულია მხოლოდ შემდეგი ტიპები: .docx .doc და .rtf", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("ფაილი ვერ მივიღეთ.", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show($"მოხდა შეცდომა. ბოდიშს გიხდით\n\nDoc Drop", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region Form Style Methods
        private void Change_SaveBtn_State(bool enabled)
        {
            Save_btn.SetVisible(enabled);
            Finnished_pic.SetVisible(enabled);
            FinnishedShape_pic.SetVisible(enabled);
            if (RecommendedDocType.Equals(".doc"))
                type_lbl.SetText(RecommendedDocType.Replace(".", string.Empty));
        }

        #endregion


        #region Form Control Methods
        private void Change_working_state(bool working = false)
        {
            try
            {
                Working = working;
                loading_box.SetVisibleAsync(Working);
                Update_Prev_Next_State(disabled: Working);
                start_btn.SetEnabled(!Working);
                Save_btn.SetEnabled(!Working);

                List<Button> btn = Pagination_Box.Controls.OfType<Button>().ToList();
                foreach (var b in btn)
                {
                    b.SetEnabledAsync(!working);
                }

                if (!working)
                    progress_lbl.SetTextAsync("გთხოვთ მოიცადოთ...");
            }
            catch
            {
                MessageBox.Show($"მოხდა შეცდომა. ბოდიშს გიხდით \n\n Change working state", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Clear_Pagination()
        {
            if (Pagination_Box.InvokeRequired)
            {
                Pagination_Box.Invoke(new MethodInvoker(delegate
                {
                    Pagination_Box.Controls.Clear();
                }));
            }
            else
                Pagination_Box.Controls.Clear();
        }
        private void Create_Pagination_Buttons(List<int> pages, int pageCount)
        {
            try
            {
                int btnHeight = Pagination_Box.GetHeight();
                var btnColor = Color.Transparent;
                var btnTextColor = Color.White;
                var btnBorderRadius = 5;
                var btnBorderColor = Color.White;
                if (CurrentPage == 1 || CurrentPage > 3 || CurrentPage >= Pages[0])
                {
                    Clear_Pagination();
                    int end = (pages.Count > 5 && pages.Last() == pageCount) ? 5 : pages.Count;
                    int index = 0;
                    if (CurrentPage == 4 && (pages.Count > 2 && pages[0] == 2))
                    {
                        Button btnFirstPage = new Button
                        {
                            Location = new System.Drawing.Point(38, 0),
                            Size = new System.Drawing.Size(33, 22),
                            BackColor = btnColor,
                            ForeColor = btnTextColor,
                            //BorderRadius = btnBorderRadius,
                            //BorderColor = btnBorderColor,
                            FlatStyle = FlatStyle.Flat,
                            Cursor = Cursors.Hand,
                            Name = $"1_btn",
                            Text = "1",
                            Enabled = !(CurrentPage == 1),
                            FlatAppearance = { BorderSize = 1, BorderColor = (CurrentPage == 1) ? Color.White : Color.Gray }
                        };
                        btnFirstPage.Click += new System.EventHandler(this.Page_btn_Click);
                        btnFirstPage.Update();
                        Pagination_Box.AddControl(btnFirstPage);
                        index = 1;
                    }
                    else if (CurrentPage > 4)
                    {
                        Button btnFirstPage = new Button
                        {
                            Location = new System.Drawing.Point(15, 0),
                            Size = new System.Drawing.Size(33, 22),
                            BackColor = btnColor,
                            ForeColor = btnTextColor,
                            //BorderRadius = btnBorderRadius,
                            //BorderColor = btnBorderColor,
                            FlatStyle = FlatStyle.Flat,
                            Cursor = Cursors.Hand,
                            Name = $"1_btn",
                            Text = "1",
                            Enabled = !(CurrentPage == 1),
                            FlatAppearance = { BorderSize = 1, BorderColor = (CurrentPage == 1) ? Color.White : Color.Gray }
                        };
                        btnFirstPage.Click += new System.EventHandler(this.Page_btn_Click);
                        btnFirstPage.Update();
                        Pagination_Box.AddControl(btnFirstPage);

                        Label sepLbl = new Label
                        {
                            Location = new System.Drawing.Point(55, 1),
                            Size = new System.Drawing.Size(20, btnHeight),
                            ForeColor = Color.White,
                            Name = "sep_lbl",
                            Text = "..."
                        };
                        Pagination_Box.AddControl(sepLbl);
                        index = 1;
                    }
                    for (int i = 1; i <= end; i++)
                    {
                        Button btnPage = new Button
                        {
                            Location = new System.Drawing.Point(38 * (i + index), 0),
                            Size = new System.Drawing.Size(37, btnHeight),
                            BackColor = btnColor,
                            ForeColor = btnTextColor,
                            //BorderRadius = btnBorderRadius,
                            //BorderColor = btnBorderColor,
                            Cursor = Cursors.Hand,
                            FlatStyle = FlatStyle.Flat,
                            Font = new Font(this.Font.FontFamily, 12),
                            Name = $"{pages[i - 1]}_btn",
                            Text = pages[i - 1].ToString(),
                            Enabled = !(CurrentPage == pages[i - 1]),
                            FlatAppearance = { BorderSize = 1, BorderColor = (CurrentPage == pages[i - 1]) ? Color.White : Color.Gray }
                        };
                        btnPage.Click += new System.EventHandler(this.Page_btn_Click);
                        btnPage.Update();
                        Pagination_Box.AddControl(btnPage);
                    }
                    if (pages.Count > 5 && end < pageCount)
                    {
                        if (pages[4] == pageCount - 1)
                        {
                            Button btnFinalPage = new Button
                            {
                                Location = new System.Drawing.Point(38 * (end + index + 1), 0),
                                Size = new System.Drawing.Size(33, 22),
                                BackColor = btnColor,
                                ForeColor = btnTextColor,
                                //BorderRadius = btnBorderRadius,
                                //BorderColor = btnBorderColor,
                                FlatStyle = FlatStyle.Flat,
                                Cursor = Cursors.Hand,
                                Name = $"{pages.Last()}_btn",
                                Text = pages.Last().ToString(),
                                Enabled = !(CurrentPage == pages.Last()),
                                FlatAppearance = { BorderSize = 1, BorderColor = (CurrentPage == pages.Last()) ? Color.White : Color.Gray }
                            };
                            btnFinalPage.Click += new System.EventHandler(this.Page_btn_Click);
                            btnFinalPage.Update();
                            Pagination_Box.AddControl(btnFinalPage);
                        }
                        else
                        {
                            Label sepLbl = new Label
                            {
                                Location = new System.Drawing.Point(38 * (end + 1 + index), 0),
                                Size = new System.Drawing.Size(20, btnHeight),
                                ForeColor = Color.White,
                                Name = "sep_lbl",
                                Text = "..."
                            };
                            Pagination_Box.AddControl(sepLbl);
                            Button btnFinalPage = new Button
                            {
                                Location = new System.Drawing.Point(36 * (end + 2 + index), 0),
                                Size = new System.Drawing.Size(33, 22),
                                BackColor = btnColor,
                                ForeColor = btnTextColor,
                                //BorderRadius = btnBorderRadius,
                                //BorderColor = btnBorderColor,
                                FlatStyle = FlatStyle.Flat,
                                Cursor = Cursors.Hand,
                                Name = $"{pages.Last()}_btn",
                                Text = pages.Last().ToString(),
                                Enabled = !(CurrentPage == pages.Last()),
                                FlatAppearance = { BorderSize = 1, BorderColor = (CurrentPage == pages.Last()) ? Color.White : Color.Gray }
                                //FlatAppearance = { BorderSize = (CurrentPage == pages.Last()) ? 1 : 0 }
                            };
                            btnFinalPage.Click += new System.EventHandler(this.Page_btn_Click);
                            btnFinalPage.Update();
                            Pagination_Box.AddControl(btnFinalPage);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show($"მოხდა შეცდომა. ბოდიშს გიხდით\n\nPagination", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Navigate_WebBrowser(string uri)
        {
            if (webBrowser.InvokeRequired)
            {
                webBrowser.Invoke(new MethodInvoker(delegate
                {
                    if (uri == string.Empty)
                        webBrowser.Navigate((Uri)null);
                    else
                        webBrowser.Navigate(uri);
                }));
            }
            else
            {
                if (uri == string.Empty)
                    webBrowser.Navigate((Uri)null);
                else
                    webBrowser.Navigate(uri);
            }
        }
        private void Change_Page(int page)
        {
            try
            {
                if (this.Controls.Find($"{CurrentPage}_btn", true).Length > 0)
                {
                    Button btn = (Button)this.Controls.Find($"{CurrentPage}_btn", true).FirstOrDefault();
                    btn.SetEnabled(true);
                    btn.FlatAppearance.BorderColor = Color.Gray;
                    btn.Update();
                }
                CurrentPage = page;
                Update_Prev_Next_State();

                string PagePath = Path.Combine(WorkingDir, $"{CurrentPage}.html");

                if (CurrentPage == 1 || CurrentPage > 3 || (Pages != null && (CurrentPage == Pages[0]) || (Pages.Count > 5 && CurrentPage == Pages[4])))
                {
                    GetPages();
                }
                if (File.Exists(PagePath))
                {
                    webBrowser.Navigate(PagePath);
                    if (this.Controls.Find($"{CurrentPage}_btn", true).Length > 0)
                    {
                        Button btn = (Button)this.Controls.Find($"{CurrentPage}_btn", true).FirstOrDefault();
                        btn.FlatAppearance.BorderColor = Color.White;
                        btn.SetEnabled(false);
                        btn.Update();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"მოხდა შეცდომა. ბოდიშს გიხდით \n {ex.Message}", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Update_Prev_Next_State(bool disabled = false)
        {
            if (CurrentPage == 1 || disabled)
                previous_btn.SetEnabled(false);
            else
                previous_btn.SetEnabled(true);

            if (CurrentPage == DocPageCount || disabled)
                next_btn.SetEnabled(false);
            else
                next_btn.SetEnabled(true);
        }

        #endregion


        #region Form Events
        private void Start_btn_Click(object sender, EventArgs e)
        {
            if (FileName != "")
            {
                try
                {
                    bool do_hyp = false;
                    DocCleanDCM dcdm = new DocCleanDCM();
                    dcdm.CleanSpaces = space_chkbx.Checked;
                    dcdm.CleanTabs = space_chkbx.Checked;
                    dcdm.CleanExcessParagraphs = par_chkbx.Checked;
                    dcdm.CleanNewLines = newline_chkbx.Checked;
                    dcdm.CorrectPDashStarts = pdashstart_chkbx.Checked;
                    do_hyp = hyp_chkbx.Checked;

                    Thread thread = new Thread(
                        delegate ()
                        {
                            try
                            {
                                if (dcdm.CleanSpaces || dcdm.CleanExcessParagraphs || dcdm.CleanNewLines || dcdm.CorrectPDashStarts)
                                {
                                    progress_lbl.SetText("მიმდინარეობს გასუფთავება...");
                                    if (!wwd.CleanDocument(dcdm))
                                        MessageBox.Show("დოკუმენტის გასუფთავების დროს მოხდა შეცდომა. ბოდიშს გიხდით", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                if (do_hyp)
                                {
                                    progress_lbl.SetText("მიმდინარეობს დამარცვლა...");
                                    if (!wwd.HyphenateDocument())
                                        MessageBox.Show("დოკუმენტის დამარცვლის დროს მოხდა შეცდომა. ბოდიშს გიხდით", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                                InitializeDocBox((string)wwd.DocPath);
                                Change_working_state(false);
                                IsSaved = false;
                                Change_SaveBtn_State(enabled: true);
                            }
                            catch
                            {
                                MessageBox.Show("დოკუმენტის დამუშავების დროს მოხდა შეცდომა. ბოდიშს გიხდით", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        });
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                    Change_working_state(true);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("მოხდა შეცდომა. ბოდიშს გიხდით", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Page_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnPager = (sender as Button);
                Change_Page(int.Parse(btnPager.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"მოხდა შეცდომა. ბოდიშს გიხდით \n {ex.Message}", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ForIndesign_chkbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (sender as CheckBox);

            newline_chkbx.SetCheckedAsync(chk.Checked);
            newline_chkbx.SetAutoCheck(!(chk.Checked));

            pdashstart_chkbx.SetCheckedAsync(chk.Checked);
            pdashstart_chkbx.SetAutoCheck(!(chk.Checked));

            RecommendedDocType = (chk.Checked) ? ".doc" : ".docx";
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!IsSaved)
                {
                    DialogResult dlgResult = MessageBox.Show("დარწმუნებული ხართ რომ არ გინდათ დამუშავებული ფაილის შენახვა?\n\nდოკუმენტზე განხორციელებული ყველა ცვლილება დაიკარგება.", "წაიშალოს ცვლილებები?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dlgResult == DialogResult.Yes)
                        Close_Doc();

                    else
                        e.Cancel = true;
                }
                else
                    if (!Working)
                    Close_Doc();
                    else
                        e.Cancel = true;
            }
            catch
            {
                MessageBox.Show($"მოხდა შეცდომა. ბოდიშს გიხდით\n\nClosing", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Close_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Working)
                {
                    if (!IsSaved)
                    {
                        DialogResult dlgResult = MessageBox.Show("დარწმუნებული ხართ რომ არ გინდათ დამუშავებული ფაილის შენახვა?\n\nდოკუმენტზე განხორციელებული ყველა ცვლილება დაიკარგება.", "წაიშალოს ცვლილებები?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dlgResult == DialogResult.Yes)
                            Close_Doc();
                    }
                    else
                        Close_Doc();
                }
            }
            catch
            {
                MessageBox.Show($"მოხდა შეცდომა. ბოდიშს გიხდით\n\nClose Request", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Save_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(FileName))
                {
                    int DefIndex = 1;
                    if (RecommendedDocType == ".doc")
                        DefIndex = 2;
                    else if (RecommendedDocType == ".rtf")
                        DefIndex = 3;
                    else if (RecommendedDocType == ".pdf")
                        DefIndex = 4;

                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        InitialDirectory = @"Documents",
                        Title = "სად შევინახოთ დამუშავებული ფაილი",
                        CheckFileExists = false,
                        CheckPathExists = false,
                        //DefaultExt = RecommendedDocType.TrimStart('.'),
                        Filter = "Word Document (*.docx)|*.docx|Word Document (*.doc)|*.doc|Rich Text Format (*.rtf)|*.rtf|Portable Document Format (*.pdf)|*.pdf",
                        FilterIndex = DefIndex,
                        RestoreDirectory = true,
                        FileName = $@"{Path.GetDirectoryName(FileName)}\{Path.GetFileNameWithoutExtension(FileName)}{RecommendedDocType}"
                    };
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        wwd.SaveDoc(saveFileDialog.FileName, Path.GetExtension(saveFileDialog.FileName));
                        IsSaved = true;
                    }
                }
                else
                {
                    MessageBox.Show("დოკუმენტი არ გვაქვს", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show($"მოხდა შეცდომა. ბოდიშს გიხდით\n\nAtSaving", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Help_btn_Click(object sender, EventArgs e)
        {
            try
            {
                HelpForm hpf = new HelpForm();
                hpf.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"მოხდა შეცდომა. ბოდიშს გიხდით", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        Size sz;
        FormWindowState LastWindowState = FormWindowState.Minimized;
        bool maxmin = false;
        protected override void OnResizeBegin(EventArgs e)
        {
            SuspendLayout();
            base.OnResizeBegin(e);
            sz = this.Size;
        }
        protected override void OnResizeEnd(EventArgs e)
        {
            if ((sz.Width != this.Size.Width || sz.Height != this.Size.Height) && WindowState == LastWindowState && !maxmin)
            {
                this.SetVisible(false);
                Doc_Panel.BorderRadius = 1;
                Doc_Panel.BorderSize = 0;
                ResumeLayout();
                base.OnResizeEnd(e);
                Doc_Panel.BorderRadius = 10;
                Doc_Panel.BorderSize = 3;
                this.SetVisible(true);
            }
            else
            {
                ResumeLayout();
            }
            LastWindowState = WindowState;
            maxmin = false;
        }
        protected override void OnResize(EventArgs e)
        {
            if (WindowState != LastWindowState || maxmin)
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    this.SetVisible(false);
                    Doc_Panel.BorderRadius = 1;
                    Doc_Panel.BorderSize = 0;
                    base.OnResize(e);
                    Doc_Panel.BorderRadius = 10;
                    Doc_Panel.BorderSize = 3;
                    this.SetVisible(true);
                }
                if (WindowState == FormWindowState.Normal)
                {
                    this.SetVisible(false);
                    Doc_Panel.BorderRadius = 1;
                    Doc_Panel.BorderSize = 0;
                    base.OnResize(e);
                    Doc_Panel.BorderRadius = 10;
                    Doc_Panel.BorderSize = 3;
                    this.SetVisible(true);
                }
                maxmin = true;
            }
        }
        //License
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            try
            {
                if (lc.Check())
                {
                    int days = (lc.days > 0) ? lc.days : 0;
                    if (days > 0)
                        this.SetTextAsync($"ჩამშვები — საცდელი (დარჩენილია {days} დღე)");
                }
                else
                {
                    limited = true;
                    this.SetTextAsync("ჩამშვები — უფასო");
                    LicenseForm lcf = new LicenseForm();
                    DialogResult result = lcf.ShowDialog();
                    if (result == DialogResult.Cancel)
                        this.Close();
                    limited = true;
                }
                if (limited)
                {
                    hyp_chkbx.SetChecked(false);
                    hyp_chkbx.SetAutoCheck(false);
                    hyp_chkbx.Click += Not_Allowed_Event;
                    ForIndesign_chkbx.SetAutoCheck(false);
                    ForIndesign_chkbx.Click += Not_Allowed_Event;
                }
            }
            catch
            {
                limited = true;
                this.SetTextAsync("ჩამშვები — უფასო");
                LicenseForm lcf = new LicenseForm();
                DialogResult result = lcf.ShowDialog();
                if (result == DialogResult.Cancel)
                    this.Close();

                hyp_chkbx.SetChecked(false);
                hyp_chkbx.SetAutoCheck(false);
                hyp_chkbx.Click += Not_Allowed_Event;
                ForIndesign_chkbx.SetAutoCheck(false);
                ForIndesign_chkbx.Click += Not_Allowed_Event;
            }
        }
        private void manual_btn_Click(object sender, EventArgs e)
        {
            try
            {
                ManualForm mf = new ManualForm(limited);
                mf.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"მოხდა შეცდომა. ბოდიშს გიხდით", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void next_btn_Click(object sender, EventArgs e)
        {
            Change_Page(CurrentPage + 1);
        }
        private void previous_btn_Click(object sender, EventArgs e)
        {
            Change_Page(CurrentPage - 1);
        }

        private void Not_Allowed_Event(object sender, EventArgs e)
        {
            MessageBox.Show("მოთხოვნილი ფუნქცია მიეკუთვნება პრემიუმ ვერსიას.", "უფასო ვერსიის შეზღუდვა", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        #endregion


    }
}

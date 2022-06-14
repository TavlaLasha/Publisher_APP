using BLL.Services;
using Models.DataControlModels;
using Models.DataViewModels;
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

        WorkWithDoc wwd;

        public MainForm()
        {
            InitializeComponent();
            this.AllowDrop = true;
            Upload_picture.Click += Upload_picture_Click;
            this.DragEnter += new DragEventHandler(Upload_picture_DragEnter);
            this.DragDrop += new DragEventHandler(Upload_picture_DragDrop);
            Pagination_Box.Left = (preview_splitContainer.Panel2.Height - Pagination_Box.Left) / 2;
            Save_btn.Left = ((preview_splitContainer.Panel2.Width + Pagination_Box.Left) / 2)+100;
            Change_SaveBtn_State(enabled: false);
        }

        #region Document Methods
        private void Upload_Doc(string file)
        {
            try
            {
                Upload_picture.SetVisibleAsync(false);
                Change_working_state(true);
                wwd = new WorkWithDoc(file);
                Change_working_state(false);
                WorkingDocPath = (string)wwd.DocPath;
                InitializeDocBox(file);
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
            type_lbl.SetTextAsync(Path.GetExtension(filename));
            filename_lbl.SetText(new FileInfo(filename).Name);

            Graphics g = this.CreateGraphics();
            Font f = filename_lbl.Font;
            string labelstr = filename_lbl.Text;
            char[] char_arr = labelstr.ToCharArray();
            int width;
            int count = 0;
            for (int i = 0; i < char_arr.Length; i++)
            {
                width = (int)(g.MeasureString(labelstr.Substring(labelstr.Length-i, i), f).Width);
                if (width >= filename_lbl.Width)
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

            if (Pages == null || !Pages.Intersect(new List<int> { CurrentPage + 1, CurrentPage + 2 }).Any() || (CurrentPage == Pages[0]) || (Pages.Count > 5 && CurrentPage == Pages[4]) || (CurrentPage==1 && !Pages.Contains(1)))
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

                        if (DocPageCount > 0)
                            page_lbl.SetTextAsync(DocPageCount.ToString());

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
                Upload_picture.SetVisible(true);
                FileName = string.Empty;
                if (CurrentPage != 0)
                {
                    wwd.CloseDoc();
                    if (Directory.Exists(WorkingDir))
                        Directory.Delete(WorkingDir, true);

                    if (File.Exists(WorkingDocPath))
                        File.Delete(WorkingDocPath);

                    Navigate_WebBrowser(FileName);
                    Pagination_Box.Controls.Clear();
                    CurrentPage = 0;
                    page_lbl.SetTextAsync("---");
                    IsSaved = true;
                    Change_SaveBtn_State(enabled: false);
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
        private void Upload_picture_Click(object sender, EventArgs e)
        {
            if (!Working && FileName == string.Empty)
            {
                try
                {
                    OpenFileDialog ofd = new OpenFileDialog()
                    {
                        InitialDirectory = @"Documents",
                        FileName = "აირჩიეთ MS Word დოკუმენტი",
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
        private void Upload_picture_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
        private void Upload_picture_DragDrop(object sender, DragEventArgs e)
        {
            if (!Working && FileName == string.Empty)
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length == 1)
                {
                    FileName = files.SingleOrDefault();
                    Upload_Doc(FileName);
                }
                else
                {
                    MessageBox.Show("მოხდა შეცდომა. ბოდიშს გიხდით", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion


        #region Form Style Methods
        private void Change_SaveBtn_State(bool enabled)
        {
            Save_btn.SetEnabled(enabled);
            Save_btn.SetForeColor(Save_btn.Enabled ? Color.Black : Color.Gray);
        }

        #endregion


        #region Form Control Methods
        private void Change_working_state(bool working = false)
        {
            Working = working;
            loading_box.SetVisibleAsync(Working);

            List<Button> btn = Pagination_Box.Controls.OfType<Button>().ToList();
            foreach (var b in btn)
            {
                b.SetEnabledAsync(!working);
            }
            if (!working)
                progress_lbl.SetTextAsync("გთხოვთ მოიცადოთ...");
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
            int btnHeight = Pagination_Box.Height;
            var btnColor = Color.White;
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
                        Size = new System.Drawing.Size(33, 20),
                        BackColor = btnColor,
                        Cursor = Cursors.Hand,
                        Name = $"1_btn",
                        Text = "1",
                        Enabled = !(CurrentPage == pages.Last()),
                        FlatAppearance = { BorderSize = (CurrentPage == pages.Last()) ? 1 : 0 }
                    };
                    btnFirstPage.Click += new System.EventHandler(this.Page_btn_Click);
                    Pagination_Box.AddControl(btnFirstPage);
                    index = 1;
                }
                else if (CurrentPage > 4)
                {
                    Button btnFirstPage = new Button
                    {
                        Location = new System.Drawing.Point(15, 0),
                        Size = new System.Drawing.Size(33, 20),
                        BackColor = btnColor,
                        Cursor = Cursors.Hand,
                        Name = $"1_btn",
                        Text = "1",
                        Enabled = !(CurrentPage == pages.Last()),
                        FlatAppearance = { BorderSize = (CurrentPage == pages.Last()) ? 1 : 0 }
                    };
                    btnFirstPage.Click += new System.EventHandler(this.Page_btn_Click);
                    Pagination_Box.AddControl(btnFirstPage);

                    Label sepLbl = new Label
                    {
                        Location = new System.Drawing.Point(55, 1),
                        Size = new System.Drawing.Size(20, btnHeight),
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
                        Size = new System.Drawing.Size(33, btnHeight),
                        BackColor = btnColor,
                        Cursor = Cursors.Hand,
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font(this.Font.FontFamily, 12),
                        Name = pages[i - 1].ToString() + "_btn",
                        Text = pages[i - 1].ToString(),
                        Enabled = !(CurrentPage == pages[i - 1]),
                        FlatAppearance = { BorderSize = (CurrentPage == pages[i - 1]) ? 1 : 0 }
                    };
                    btnPage.Click += new System.EventHandler(this.Page_btn_Click);

                    Pagination_Box.AddControl(btnPage);
                }
                if (pages.Count > 5 && end < pageCount)
                {
                    if (pages[4] == pageCount - 1)
                    {
                        Button btnFinalPage = new Button
                        {
                            Location = new System.Drawing.Point(38 * (end + 1), 0),
                            Size = new System.Drawing.Size(33, 20),
                            BackColor = btnColor,
                            Cursor = Cursors.Hand,
                            Name = $"{pages.Last()}_btn",
                            Text = pages.Last().ToString(),
                            Enabled = !(CurrentPage == pages.Last()),
                            FlatAppearance = { BorderSize = (CurrentPage == pages.Last()) ? 1 : 0 }
                        };
                        btnFinalPage.Click += new System.EventHandler(this.Page_btn_Click);
                        Pagination_Box.AddControl(btnFinalPage);
                    }
                    else
                    {
                        Label sepLbl = new Label
                        {
                            Location = new System.Drawing.Point(38 * (end + 1 + index), 0),
                            Size = new System.Drawing.Size(20, btnHeight),
                            Name = "sep_lbl",
                            Text = "..."
                        };
                        Pagination_Box.AddControl(sepLbl);
                        Button btnFinalPage = new Button
                        {
                            Location = new System.Drawing.Point(36 * (end + 2 + index), 0),
                            Size = new System.Drawing.Size(33, 20),
                            BackColor = btnColor,
                            Cursor = Cursors.Hand,
                            Name = $"{pages.Last()}_btn",
                            Text = pages.Last().ToString(),
                            Enabled = !(CurrentPage == pages.Last()),
                            FlatAppearance = { BorderSize = (CurrentPage == pages.Last()) ? 1 : 0 }
                        };
                        btnFinalPage.Click += new System.EventHandler(this.Page_btn_Click);
                        Pagination_Box.AddControl(btnFinalPage);
                    }
                }
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

        #endregion


        #region Form Events
        private void Start_btn_Click(object sender, EventArgs e)
        {
            if (FileName != "")
            {
                bool do_hyp = false;
                DocCleanDCM dcdm = new DocCleanDCM();
                dcdm.CleanSpaces = space_chkbx.Checked;
                dcdm.CleanExcessParagraphs = par_chkbx.Checked;
                dcdm.CleanNewLines = newline_chkbx.Checked;
                dcdm.CorrectPDashStarts = pdashstart_chkbx.Checked;
                dcdm.CleanTabs = tab_chkbx.Checked;
                do_hyp = hyp_chkbx.Checked;

                try
                {
                    Thread thread = new Thread(
                        delegate ()
                        {
                            try
                            {
                                if (dcdm.CleanSpaces || dcdm.CleanExcessParagraphs || dcdm.CleanNewLines || dcdm.CorrectPDashStarts || dcdm.CleanTabs)
                                {
                                    progress_lbl.SetTextAsync("მიმდინარეობს გასუფთავება...");
                                    if (!wwd.CleanDocument(dcdm))
                                    {
                                        MessageBox.Show("დოკუმენტის გასუფთავების დროს მოხდა შეცდომა. ბოდიშს გიხდით", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                if (do_hyp)
                                {
                                    progress_lbl.SetTextAsync("მიმდინარეობს დამარცვლა...");
                                    if (!wwd.HypernateDocument())
                                    {
                                        MessageBox.Show("დოკუმენტის დამარცვლის დროს მოხდა შეცდომა. ბოდიშს გიხდით", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }

                                InitializeDocBox((string)wwd.DocPath);
                                Change_working_state(false);
                                IsSaved = false;
                                Change_SaveBtn_State(enabled: true);
                                //MessageBox.Show($"ფაილი {Path.GetFileName(FileName)} წარმატებით დამუშავდა.", "წარმატება", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (this.Controls.Find($"{CurrentPage}_btn", true).Length > 0)
                {
                    Button btn = (Button)this.Controls.Find($"{CurrentPage}_btn", true).FirstOrDefault();
                    btn.SetEnabled(true);
                    btn.FlatAppearance.BorderSize = 0;
                }
                CurrentPage = int.Parse(btnPager.Text);
                string PagePath = Path.Combine(WorkingDir, $"{CurrentPage}.html");

                if (CurrentPage == 1 || CurrentPage > 3 || (Pages != null && (CurrentPage == Pages[0]) || (Pages.Count > 5 && CurrentPage == Pages[4])))
                {
                    GetPages();
                }
                if (File.Exists(PagePath))
                {
                    webBrowser.Navigate(PagePath);
                    btnPager.SetEnabled(false);
                    btnPager.FlatAppearance.BorderSize = 1;
                }
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
            newline_chkbx.SetEnabledAsync(!(chk.Checked));

            pdashstart_chkbx.SetCheckedAsync(chk.Checked);
            pdashstart_chkbx.SetEnabledAsync(!(chk.Checked));

            tab_chkbx.SetCheckedAsync(chk.Checked);
            tab_chkbx.SetEnabledAsync(!(chk.Checked));
            RecommendedDocType = (chk.Checked) ? ".doc" : ".docx";
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
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
                if(!Working)
                    Close_Doc();
                else
                    e.Cancel = true;
        }
        private void Close_btn_Click(object sender, EventArgs e)
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
        private void Save_btn_Click(object sender, EventArgs e)
        {
            if (File.Exists(FileName))
            {
                int DefIndex = 1;
                if (RecommendedDocType == ".doc")
                    DefIndex = 2;
                else if (RecommendedDocType == ".rtf")
                    DefIndex = 3;

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    InitialDirectory = @"Documents",
                    Title = "სად შევინახოთ დამუშავებული ფაილი",
                    CheckFileExists = false,
                    CheckPathExists = false,
                    //DefaultExt = RecommendedDocType.TrimStart('.'),
                    Filter = "Word Document (*.docx)|*.docx |Word Document (*.doc)|*.doc |Rich Text Format (*.rtf)|*.rtf",
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
        private void Help_btn_Click(object sender, EventArgs e)
        {
            HelpForm hpf = new HelpForm();
            hpf.ShowDialog();
        }

        #endregion
    }
}

using BLL.Services;
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
        //private delegate void SafeCallDelegate(bool visibility);
        private bool Working = false;
        private bool IsSaved = true;

        private bool cl_splace = false;
        private bool cl_newLines = false;
        private bool cor_PDashStarts = false;
        private bool cl_tabs = false;
        private bool do_hyp = false;

        private string WorkingDir;
        private string WorkingDocPath;
        private int CurrentPage=0;
        private string RecommendedDocType = ".docx";

        WorkWithDoc wwd;

        public MainForm()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(MainForm_DragEnter);
            this.DragDrop += new DragEventHandler(MainForm_DragDrop);
        }

        private void Start_btn_Click(object sender, EventArgs e)
        {
            if (FileName != "")
            {
                cl_splace = space_chkbx.Checked;
                cl_newLines = newline_chkbx.Checked;
                cor_PDashStarts = pdashstart_chkbx.Checked;
                cl_tabs = tab_chkbx.Checked;
                do_hyp = hyp_chkbx.Checked;

                try
                {
                    Thread thread = new Thread(
                        delegate ()
                        {
                            try
                            {
                                if (cl_splace || cl_newLines || cor_PDashStarts || cl_tabs)
                                {
                                    if (!wwd.CleanDocument(cl_splace, cl_newLines, cor_PDashStarts, cl_tabs))
                                    {
                                        MessageBox.Show("დოკუმენტის გასუფთავების დროს მოხდა შეცდომა. ბოდიშს გიხდით", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                if (do_hyp)
                                {
                                    if (!wwd.HypernateDocument())
                                    {
                                        MessageBox.Show("დოკუმენტის დამარცვლის დროს მოხდა შეცდომა. ბოდიშს გიხდით", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            finally
                            {
                                InitializeDocBox((string)wwd.DocPath);
                                Change_working_state(false);
                                IsSaved = false;
                                MessageBox.Show($"ფაილი {Path.GetFileName(FileName)} წარმატებით დამუშავდა.", "წარმატება", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        });
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                    Change_working_state(true);
                                            
                }
                catch(Exception ex)
                {
                    MessageBox.Show("მოხდა შეცდომა. ბოდიშს გიხდით", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MainForm_Click(object sender, EventArgs e)
        {
            if (!Working)
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

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (!Working)
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

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
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

        private void InitializeDocBox(string filename)
        {
            size_lbl.SetText((new FileInfo(filename).Length / 1000.0).ToString() + " Kb");
            type_lbl.SetText(Path.GetExtension(filename));
            filename_lbl.SetText(new FileInfo(filename).Name);
            docBox.SetVisible(true);
            GetPages();
        }

        private void Close_btn_Click(object sender, EventArgs e)
        {
            if (!IsSaved)
            {
                DialogResult dlgResult = MessageBox.Show("დარწმუნებული ხართ რომ არ გინდათ დამუშავებული ფაილის შენახვა?", "წაიშალოს ცვლილებები?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(dlgResult == DialogResult.Yes)
                    Close_Doc();
            }
            else
                Close_Doc();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsSaved)
            {
                DialogResult dlgResult = MessageBox.Show("დარწმუნებული ხართ რომ არ გინდათ დამუშავებული ფაილის შენახვა?", "წაიშალოს ცვლილებები?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dlgResult == DialogResult.Yes)
                    Close_Doc();

                else
                    e.Cancel = true;
            }
            else
                Close_Doc();
        }

        private void Upload_Doc(string file)
        {
            try
            {
                Change_working_state(true);
                wwd = new WorkWithDoc(file);
                Change_working_state(false);
                WorkingDocPath = (string)wwd.DocPath;
                InitializeDocBox(file);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"მოხდა შეცდომა. ბოდიშს გიხდით \n {ex.Message}", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Change_working_state(bool working = false)
        {
            Working = working;
            loading_img.SetVisible(Working);
        }

        private void Close_Doc()
        {
            docBox.SetVisible(false);
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
                IsSaved = true;
            }
        }

        private void Paginate(List<int> pages, int docPageCount)
        {
            if (Directory.Exists(WorkingDir))
                if(CurrentPage == 1)
                    Create_Pagination_Buttons(1, pages.Count-1, docPageCount);
        }

        private void Create_Pagination_Buttons(int start, int end, int final)
        {
            Clear_Pagination();

            for (int i = start; i <= end; i++)
            {
                Button btnPage = new Button
                {
                    Location = new System.Drawing.Point(38 * i, 10),
                    Size = new System.Drawing.Size(30, 20),
                    Name = i.ToString() + "_btn",
                    Text = i.ToString(),
                    Enabled = !(CurrentPage == i)
                };
                btnPage.Click += new System.EventHandler(this.Page_btn_Click);

                Pagination_Box.AddControl(btnPage);
            }
            if (!(end == final))
            {
                Label sepLbl = new Label
                {
                    Location = new System.Drawing.Point(38 * (end + 1), 10),
                    Size = new System.Drawing.Size(20, 20),
                    Name = "sep_lbl",
                    Text = "..."
                };
                Pagination_Box.AddControl(sepLbl);
                Button btnFinalPage = new Button
                {
                    Location = new System.Drawing.Point(36 * (end + 2), 10),
                    Size = new System.Drawing.Size(35, 20),
                    Name = "final_btn",
                    Text = final.ToString(),
                    Enabled = !(CurrentPage == final)
                };
                btnFinalPage.Click += new System.EventHandler(this.Page_btn_Click);
                Pagination_Box.AddControl(btnFinalPage);
            }
            //else
            //{
            //    Button btnFinalPage = new Button
            //    {
            //        Location = new System.Drawing.Point(38 * end, 5),
            //        Size = new System.Drawing.Size(35, 20),
            //        Name = "final_btn",
            //        Text = end.ToString(),
            //        Enabled = !(CurrentPage == end)
            //    };
            //    btnFinalPage.Click += new System.EventHandler(this.Page_btn_Click);
            //    Pagination_Box.Controls.Add(btnFinalPage);
            //}
        }

        private void Page_btn_Click(object sender, EventArgs e)
        {
            Button btnPager = (sender as Button);
            this.Controls.Find($"{CurrentPage}_btn", true).FirstOrDefault().Enabled = true;
            CurrentPage = int.Parse(btnPager.Text);
            btnPager.Enabled = false;
            if (CurrentPage > 3)
            {

            }
            string PagePath = Path.Combine(WorkingDir, $"{CurrentPage}.html");
            if(File.Exists(PagePath))
                webBrowser.Navigate(PagePath);
        }

        private void GetPages()
        {
            List<int> Pages = null;
            Thread thread = new Thread
                (delegate ()
                {
                    try
                    {
                        Change_working_state(true);
                        CurrentPage = (CurrentPage == 0) ? 1 : CurrentPage;
                        DocDTO result = wwd.GetPages(CurrentPage);
                        WorkingDir = result.TempDirectory;
                        DocPageCount = result.PageCount;
                        Pages = result.Pages;
                        
                        string FilePath = Path.Combine(WorkingDir, $"{CurrentPage}.html");
                        if (File.Exists(FilePath) && Pages.Count > 0)
                        {
                            Navigate_WebBrowser(FilePath);

                            Paginate(Pages, DocPageCount);
                        }
                        Change_working_state(false);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("მოხდა შეცდომა. ბოდიშს გიხდით. \n\n Err_InitDocBox", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void ForIndesign_chkbx_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (sender as CheckBox);

            newline_chkbx.SetChecked(chk.Checked);
            newline_chkbx.SetEnabled(!(chk.Checked));

            pdashstart_chkbx.SetChecked(chk.Checked);
            pdashstart_chkbx.SetEnabled(!(chk.Checked));

            space_chkbx.SetChecked(chk.Checked);
            space_chkbx.SetEnabled(!(chk.Checked));

            tab_chkbx.SetChecked(chk.Checked);
            tab_chkbx.SetEnabled(!(chk.Checked));
            RecommendedDocType = (chk.Checked) ? ".doc" : ".docx";
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
    }
}

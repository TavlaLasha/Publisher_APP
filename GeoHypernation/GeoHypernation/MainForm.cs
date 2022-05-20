﻿using BLL.Services;
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
        private bool working = false;

        private bool cl_splace = false;
        private bool cl_newLines = false;
        private bool cor_PDashStarts = false;
        private bool cl_tabs = false;
        private bool do_hyp = false;

        private string workingDir;
        private int CurrentPage=1;

        WorkWithDoc wwd;

        public MainForm()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(MainForm_DragEnter);
            this.DragDrop += new DragEventHandler(MainForm_DragDrop);
            //webBrowser.DocumentText = $"<html><head></head><body><img style='width:100%' src='{Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, "Images\\LOGO.jpg")}'/></body></html>";
        }

        private void Start_btn_Click(object sender, EventArgs e)
        {
            if (FileName != "")
            {
                cl_splace = space_chkbx.Checked;
                cl_newLines = tab_chkbx.Checked;
                cor_PDashStarts = tab_chkbx.Checked;
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
                                Change_working_state(false);
                                MessageBox.Show($"ფაილი {Path.GetFileName(FileName)} წარმატებით დამუშავდა და შეინახა მითითებულ ადგილას.", "წარმატება", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (!working)
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
                        wwd = new WorkWithDoc(FileName);
                        InitializeDocBox(FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (!working)
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length == 1)
                {
                    FileName = files.SingleOrDefault();
                    wwd = new WorkWithDoc(FileName);
                    InitializeDocBox(FileName);
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
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = @"Documents",
                Title = "სად შევინახოთ დამუშავებული ფაილი",
                CheckFileExists = false,
                CheckPathExists = true,
                DefaultExt = ".docx",
                Filter = "Word Document (*.docx)|*.docx|Word Document (*.doc)|*.doc|Rich Text Format (*.rtf)|*.rtf",
                FilterIndex = 0,
                RestoreDirectory = true,
                FileName = FileName
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

            }
            else
            {
                MessageBox.Show("დოკუმენტი არ გვაქვს", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeDocBox(string filename)
        {
            size_lbl.Text = (new FileInfo(filename).Length / 1000.0).ToString() + " Kb";
            type_lbl.Text = Path.GetExtension(filename);
            filename_lbl.Text = new FileInfo(filename).Name;
            docBox.Visible = true;
            Thread thread = new Thread(
                            delegate ()
                            {
                                try
                                {
                                    Change_working_state(true);
                                    DocDTO result = wwd.GetPages(1);
                                    workingDir = result.FileName;
                                    DocPageCount = result.PageCount;
                                }
                                catch
                                {
                                    MessageBox.Show("მოხდა შეცდომა. ბოდიშს გიხდით", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                finally
                                {
                                    string FilePath = Path.Combine(workingDir, "1.html");
                                    if (File.Exists(FilePath))
                                    {
                                        if (webBrowser.InvokeRequired)
                                        {
                                            webBrowser.Invoke(new MethodInvoker(delegate
                                            {
                                                webBrowser.Navigate(FilePath);
                                            }));
                                        }
                                        CurrentPage = 1;
                                        Paginate();
                                    }
                                    Change_working_state(false);
                                }
                            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void Close_btn_Click(object sender, EventArgs e)
        {
            Close_Doc();
        }

        private void Change_working_state(bool working = false)
        {
            this.working = working;
            if (working)
            {
                if (loading_img.InvokeRequired)
                {
                    loading_img.Invoke(new MethodInvoker(delegate
                    {
                        loading_img.Visible = true;
                    }));
                }
                else
                {
                    loading_img.Visible = true;
                }
            }
            else
            {
                if (loading_img.InvokeRequired)
                {
                    loading_img.Invoke(new MethodInvoker(delegate
                    {
                        loading_img.Visible = false;
                    }));
                }
                else
                {
                    loading_img.Visible = false;
                }
            }
        }

        private void Close_Doc()
        {
            if (docBox.InvokeRequired)
            {
                docBox.Invoke(new MethodInvoker(delegate
                {
                    docBox.Visible = false;
                }));
            }
            else
            {
                docBox.Visible = false;
            }
            FileName = String.Empty;
            if (Directory.Exists(workingDir))
            {
                Directory.Delete(workingDir, true);
            }
            if (webBrowser.InvokeRequired)
            {
                webBrowser.Invoke(new MethodInvoker(delegate
                {
                    webBrowser.Navigate((Uri)null);
                }));
            }
            else
            {
                webBrowser.Navigate((Uri)null);
            }
        }

        private void Paginate()
        {
            if (Directory.Exists(workingDir))
            {
                if(CurrentPage == 1)
                {
                    Create_Pagination_Buttons(1, 4, 4);
                }
            }
        }

        private void Create_Pagination_Buttons(int start, int end, int final)
        {
            //Pagination_Box.Controls.Clear();

            for(int i=start; i<=end; i++)
            {
                Button btnPage = new Button();
                btnPage.Location = new System.Drawing.Point(38 * i, 5);
                btnPage.Size = new System.Drawing.Size(35, 20);
                btnPage.Name = i.ToString()+"_btn";
                btnPage.Text = i.ToString();
                btnPage.Enabled = !(CurrentPage == i);
                btnPage.Click += new System.EventHandler(this.Page_btn_Click);

                //Pagination_Box.Controls.Add(btnPage);
                if (Pagination_Box.InvokeRequired)
                {
                    Pagination_Box.Invoke(new MethodInvoker(delegate
                    {
                        Pagination_Box.Controls.Add(btnPage);
                    }));
                }
            }
        }

        private void Page_btn_Click(object sender, EventArgs e)
        {
            Button btnPager = (sender as Button);
            string PagePath = Path.Combine(workingDir, $"{int.Parse(btnPager.Text)}.html");
            if(File.Exists(PagePath))
                webBrowser.Navigate(PagePath);
        }
    }
}

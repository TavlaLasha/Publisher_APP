using GeoHypernation.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoHypernation
{
    public partial class MainForm : Form
    {
        private string FileName = "";
        private delegate void SafeCallDelegate(bool visibility);
        private bool working = false;
        public MainForm()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(MainForm_DragEnter);
            this.DragDrop += new DragEventHandler(MainForm_DragDrop);
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            if (FileName != "")
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    InitialDirectory = @"Documents",
                    Title = "Save Hypernated Document",
                    CheckFileExists = false,
                    CheckPathExists = true,
                    DefaultExt = ".docx",
                    Filter = "Word Document (*.docx)|*.docx|Word Document (*.doc)|*.doc|Rich Text Format (*.rtf)|*.rtf",
                    //Filter = "Word Document (*.xml)|*.xml",
                    FilterIndex = 0,
                    RestoreDirectory = true,
                    FileName = FileName
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {

                        WorkWithDoc wwd = new WorkWithDoc();
                        //ThreadStart threadStart = new ThreadStart();
                        Thread thread = new Thread(
                            delegate ()
                            {
                                try
                                {
                                    wwd.ProcessWordDocument(@"C:\Users\lasha\Desktop\demo2.docx", saveFileDialog.FileName);
                                }
                                finally
                                {
                                    if (loading_img.InvokeRequired)
                                    {
                                        loading_img.Invoke(new MethodInvoker(delegate
                                        {
                                            loading_img.Visible = false;
                                        }));
                                    }
                                    MessageBox.Show("ოპერაცია წარმატებულია!", "წარმატება", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    if (docBox.InvokeRequired)
                                    {
                                        docBox.Invoke(new MethodInvoker(delegate
                                        {
                                            docBox.Visible = false;
                                        }));
                                    }
                                    working = false;
                                }
                            });
                        thread.SetApartmentState(ApartmentState.MTA);
                        thread.Start();
                        loading_img.Visible = true;
                        working = true;

                        Console.WriteLine(saveFileDialog.FileName);
                        
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("მოხდა შეცდომა. ბოდიშს გიხდით", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No File Selected", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_Click(object sender, EventArgs e)
        {
            if (!working)
            {
                try
                {
                    OpenFileDialog openFileDialog1 = new OpenFileDialog()
                    {
                        FileName = "Select MS Word Document",
                        //Filter = "Word Document (*.xml)|*.xml",
                        Filter = "Word Document (*.docx)|*.docx|Word Document (*.doc)|*.doc|Rich Text Format (*.rtf)|*.rtf",
                        Title = "Open MS Word Document"
                    };
                    DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
                    if (result == DialogResult.OK) // Test result.
                    {
                        FileName = openFileDialog1.FileName;
                        initializeDocBox(FileName);
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
                    initializeDocBox(FileName);
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void initializeDocBox(string filename)
        {
            size_lbl.Text = (new FileInfo(filename).Length / 1000.0).ToString() + " Kb";
            type_lbl.Text = Path.GetExtension(filename);
            filename_lbl.Text = new FileInfo(filename).Name;
            docBox.Visible = true;
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            docBox.Visible = false;
        }

    }
}

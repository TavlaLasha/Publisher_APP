using BLL.Services;
using Models.DataControlModels;
using Models.DataViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoHypernation
{
    public partial class ManualForm : Form
    {
        private bool Working = false;
        private string Text;
        WorkWithDoc wwd;
        public ManualForm()
        {
            InitializeComponent();
            Text = string.Empty;
        }

        private void Start_btn_Click(object sender, EventArgs e)
        {
            progress_lbl.SetTextAsync("");
            Text = richTextBox.Text;
            if (!string.IsNullOrEmpty(Text))
            {
                try
                {
                    wwd = new WorkWithDoc(new TextDTO() { Text = Text });

                    bool do_hyp = false;
                    DocCleanDCM dcdm = new DocCleanDCM();
                    dcdm.CleanSpaces = space_chkbx.Checked;
                    dcdm.CleanTabs = space_chkbx.Checked;
                    dcdm.CleanExcessParagraphs = par_chkbx.Checked;
                    dcdm.CorrectPDashStarts = pdashstart_chkbx.Checked;
                    do_hyp = hyp_chkbx.Checked;

                    Thread thread = new Thread(
                        delegate ()
                        {
                            try
                            {
                                if (dcdm.CleanSpaces || dcdm.CleanExcessParagraphs || dcdm.CorrectPDashStarts)
                                {
                                    //progress_lbl.SetTextAsync("მიმდინარეობს გასუფთავება...");
                                    Text = wwd.CleanText(dcdm);
                                    if (string.IsNullOrEmpty(Text))
                                    {
                                        MessageBox.Show("დოკუმენტის გასუფთავების დროს მოხდა შეცდომა. ბოდიშს გიხდით", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                if (do_hyp)
                                {
                                    //progress_lbl.SetTextAsync("მიმდინარეობს დამარცვლა...");
                                    Text = wwd.HypernateText();
                                    if (string.IsNullOrEmpty(Text))
                                    {
                                        MessageBox.Show("დოკუმენტის დამარცვლის დროს მოხდა შეცდომა. ბოდიშს გიხდით", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }

                                //Change_working_state(false);
                                richTextBox.SetTextAsync(Text);
                            }
                            catch
                            {
                                MessageBox.Show("დოკუმენტის დამუშავების დროს მოხდა შეცდომა. ბოდიშს გიხდით", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        });
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                    //Change_working_state(true);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("მოხდა შეცდომა. ბოდიშს გიხდით", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                progress_lbl.SetTextAsync("გთხოვთ შეიყვანოთ ტექსტი");
        }

        //private void Change_working_state(bool working = false)
        //{
        //    Working = working;
        //    //loading_box.SetVisibleAsync(Working);

        //    if (working)
        //        progress_lbl.SetTextAsync("გთხოვთ მოიცადოთ...");
        //    else
        //        progress_lbl.SetTextAsync("");
        //}

        private void richTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control && e.KeyCode == Keys.V)
                {
                    richTextBox.Text += (string)Clipboard.GetText();
                    e.Handled = true;
                }
                if (e.Control && e.KeyCode == Keys.C)
                {
                    Clipboard.SetText(richTextBox.Text);
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("მოხდა შეცდომა. ბოდიშს გიხდით\n\nBoxKeyDown", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void paste_btn_Click(object sender, EventArgs e)
        {
            richTextBox.Text += (string)Clipboard.GetText();
        }

        private void copy_btn_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox.Text);
        }

        private void info_btn_Click(object sender, EventArgs e)
        {
            try
            {
                HelpForm hpf = new HelpForm();
                hpf.ShowDialog();
            }
            catch
            {
                MessageBox.Show($"მოხდა შეცდომა. ბოდიშს გიხდით\n\nOnHelp", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

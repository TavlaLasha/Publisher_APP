using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoHypernation
{
    public partial class LicenseForm : Form
    {
        private Timer timer;
        private int counter = 10;
        public LicenseForm()
        {
            InitializeComponent();
            timer_lbl.Text = counter.ToString();
        }

        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            timer = new Timer();
            timer.Interval = (1000);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            // start processing emails

            // when finished 
            //timer.Stop();
            //sw.Stop();
            //timer_lbl.Text = "Completed in " + sw.Elapsed.Seconds.ToString() + "seconds";
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            counter--;
            if (counter == 0)
            {
                timer.Stop();
                continue_btn.Enabled = true;
            }
            timer_lbl.Text = counter.ToString();
        }
    }
}

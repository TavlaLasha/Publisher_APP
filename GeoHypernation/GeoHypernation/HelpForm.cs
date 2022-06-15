using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoHypernation
{
    public partial class HelpForm : Form
    {
        readonly string exeDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        
        public HelpForm()
        {
            InitializeComponent();
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            string InfoPath = Path.Combine(exeDir, "info.rtf");
            if (File.Exists(InfoPath))
                Info_listBox.LoadFile(InfoPath);
            else
                Info_listBox.Text = "შეცდომა. საინფორმაციო ფაილი ვერ მოიძებნა";
        }
    }
}

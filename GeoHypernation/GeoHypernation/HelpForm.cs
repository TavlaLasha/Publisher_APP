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
        readonly string exeDir = Path.GetDirectoryName((new System.Uri(Assembly.GetEntryAssembly().CodeBase)).AbsolutePath);
        public HelpForm()
        {
            InitializeComponent();
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            string data = File.ReadAllText(Path.Combine(exeDir,"sample.html"));
            //string jsonString = File.ReadAllText(Path.Combine(exeDir,"info.json"));
            //Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
            //foreach (KeyValuePair<string, string> i in data)
            //{
            //    Info_listBox.Text+=($"{i.Key}: {i.Value}");
            //}
            Info_listBox.LoadFile(Path.Combine(exeDir, "test.rtf"));
        }
    }
}

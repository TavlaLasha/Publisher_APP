using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataControlModels
{
    public class DocCleanDCM
    {
        public bool CleanSpaces { set; get; }
        public bool CleanExcessParagraphs { set; get; }
        public bool CleanNewLines { set; get; }
        public bool CleanTabs { set; get; }
        public bool CorrectPDashStarts { set; get; }
    }
}

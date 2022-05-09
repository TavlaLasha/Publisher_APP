using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HypBLL.Interfaces
{
    public interface IWorkWithDoc
    {
        bool HypernateDocument(object filename, object saveAs);
        string[] GetPages(object filename, int page = 1);
        bool ConvertToPDF(string input, string output, WdSaveFormat format);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface IWorkWithDoc
    {
        bool HypernateDocument();
        bool CleanDocument(bool cl_splace, bool cl_newLines, bool cor_PDashStarts, bool cl_tabs);
        //string[] GetPages(object filename, int page = 1);
        bool ConvertToPDF(string input, string output, Microsoft.Office.Interop.Word.WdSaveFormat format);
        //bool ZipUpFiles(string dirPath, string outputPath);
    }
}

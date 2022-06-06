using Microsoft.Office.Interop.Word;
using Models.DataViewModels.DocManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface IDocManagement
    {
        bool HyphenateDocument(object filename, object saveAs);
        string HyphenateText(string text);
        bool CleanDocument(object filename, object saveAs, DocCleanDTO docclDTo);
        string CleanText(string text, DocCleanDTO docclDTo);
        string[] GetPages(object filename, int page, bool clean);
        bool ConvertToPDF(string input, string output, WdSaveFormat format);
        bool ZipUpFiles(string dirPath, string outputPath);
    }
}

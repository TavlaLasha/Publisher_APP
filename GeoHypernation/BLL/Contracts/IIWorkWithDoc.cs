using Models.DataViewModels;
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
        DocDTO GetPages(int page);
        bool ConvertToPDF(string input, string output, Microsoft.Office.Interop.Word.WdSaveFormat format);
    }
}

using Microsoft.Office.Interop.Word;
using Models.DataViewModels.DocManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface ICleanManagement
    {
        Application Execute(DocCleanDTO docClean);
        Application CleanSpaces(Application wordApp);
        Application CleanNewLines(Application wordApp);
        Application CleanExcParagraphs(Application wordApp);
        Application CleanTabs(Application wordApp);
        Application CorrectPDashStarts(Application wordApp);
        string ExecuteTxt(DocCleanDTO docClean);
        string CleanSpaces(string text);
        string CleanExcParagraphs(string text);
        string CleanTabs(string text);
        string CorrectPDashStarts(string text);
    }
}

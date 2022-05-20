using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface ICleanDoc
    {
        Application Execute(bool cl_splace, bool cl_newLines, bool cor_PDashStarts, bool cl_tabs);
        Application CleanSpaces(Application wordApp);
        Application CleanNewLines(Application wordApp);
        Application CleanTabs(Application wordApp);
        Application CorrectPDashStarts(Application wordApp);
        Application FindAndReplace(Application wordApp, object toFindText, object replaceWithText);
    }
}

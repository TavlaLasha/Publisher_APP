using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IHYPManagement
    {
        Application CleanOldHyp(Application wordApp);
        Application HYPConsonants(Application wordApp);
        Application HYPWovels(Application wordApp);
        Application CleanFirst(Application wordApp);
        Application CleanLast(Application wordApp);
        Application CleanConstr(Application wordApp);
        Application CleanHarmonics(Application wordApp);
        Application CleanLastConpunct(Application wordApp);
        Application HYPExecute();
        Application FindAndReplace(Application wordApp, object toFindText, object replaceWithText);
        string CleanOldHyp(string text);
        string HYPConsonants(string text);
        string HYPWovels(string text);
        string CleanFirst(string text);
        string CleanLast(string text);
        string CleanConstr(string text);
        string CleanHarmonics(string text);
        string CleanLastConpunct(string text);
        string HYPExecuteTxt();
    }
}

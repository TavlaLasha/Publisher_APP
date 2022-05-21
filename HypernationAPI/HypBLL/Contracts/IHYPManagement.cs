using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HypBLL.Interfaces
{
    public interface IHYP
    {
        Application HYPConsonants(Application wordApp);
        Application HYPWovels(Application wordApp);
        Application CleanFirst(Application wordApp);
        Application CleanLast(Application wordApp);
        Application CleanConstr(Application wordApp);
        Application CleanHarmonics(Application wordApp);
        Application CleanLastConpunct(Application wordApp);
        Application HYPExecute();
        Application FindAndReplace(Application wordApp, object toFindText, object replaceWithText);
    }
}

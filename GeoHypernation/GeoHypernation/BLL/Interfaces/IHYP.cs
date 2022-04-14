using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace GeoHypernation.BLL.Interfaces
{
    internal interface IHYP
    {
        Application HYPconsonants(Application wordApp);
        Application HYPwovels(Application wordApp);
        Application HYPcleanfirst(Application wordApp);
        Application HYPcleanlast(Application wordApp);
        Application HYPcleanlastconpunct(Application wordApp);
        Application HYPExecute(Application wordApp);
        Application FindAndReplace(Application wordApp, object toFindText, object replaceWithText);
    }
}

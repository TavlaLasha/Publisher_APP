using Microsoft.Office.Interop.Word;

namespace HypernationAPI.BLL.Interfaces
{
    public interface IHYP
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

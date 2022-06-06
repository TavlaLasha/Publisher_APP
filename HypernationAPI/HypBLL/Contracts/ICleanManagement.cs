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
        Application CleanTabs(Application wordApp);
        Application CorrectPDashStarts(Application wordApp);
    }
}

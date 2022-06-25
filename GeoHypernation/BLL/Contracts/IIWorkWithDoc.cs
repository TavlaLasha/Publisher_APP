using Models.DataControlModels;
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
        bool HyphenateDocument();
        bool CleanDocument(DocCleanDCM docClean);
        DocDTO GetPages(int page, bool clean);
    }
}

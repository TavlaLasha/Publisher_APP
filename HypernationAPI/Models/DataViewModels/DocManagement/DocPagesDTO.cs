using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataViewModels.DocManagement
{
    public class DocPagesDTO
    {
        public int PageCount { set; get; }
        public Dictionary<int, string> Pages { set; get; }
    }
}

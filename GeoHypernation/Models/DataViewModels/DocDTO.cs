using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataViewModels
{
    public class DocDTO
    {
        public string FileName { set; get; }
        public long FileSize { set; get; }
        public int PageCount { set; get; }
    }
}

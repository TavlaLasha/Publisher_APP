using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataViewModels.WordManagement
{
    public class BarbarismDTO
    {
        public int Id { set; get; }
        public string Correct_Word { set; get; }
        public string Wrong_Word { set; get; }
        public string Description { set; get; }
    }
}

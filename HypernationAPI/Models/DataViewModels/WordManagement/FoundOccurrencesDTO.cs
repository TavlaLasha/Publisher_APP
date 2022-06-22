using Models.DataViewModels.DocManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataViewModels.WordManagement
{
    public class FoundOccurrencesDTO
    {
        public TextDTO textDTO { set; get; }
        public Dictionary<string,int> occurrences { set; get; }
    }
}

using Models.DataViewModels.WordManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface IBarbarismManagement
    {
        IEnumerable<BarbarismDTO> GetAllBarbarisms();
        BarbarismDTO GetBarbarism(int id);
        BarbarismDTO GetBarbarism(string wrong_word);
        bool AddBarbarism(BarbarismDTO brbDTO);
        bool EditBarbarism(string id, BarbarismDTO brbDTO);
        bool DeleteBarbarism(string id);
        FoundOccurrencesDTO FindBarbarisms(string text);
    }
}

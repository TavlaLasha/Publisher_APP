using Models.DataViewModels.WordManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Contracts
{
    public interface IBarbarismRepository : IDisposable
    {
        IEnumerable<BarbarismDTO> GetAllBarbarisms();
        BarbarismDTO GetBarbarism(int id);
        bool ExistsBarbarismId(int id);
        bool ExistsBarbarism(string wrong_word);
        bool AddBarbarism(BarbarismDTO brbDTO);
        bool EditBarbarism(int id, BarbarismDTO brbDTO);
        bool DeleteBarbarism(int id);
        void SaveChanges();
    }
}

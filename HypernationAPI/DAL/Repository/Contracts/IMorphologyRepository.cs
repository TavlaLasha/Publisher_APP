using Models.DataViewModels.WordManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Contracts
{
    public interface IMorphologyRepository : IDisposable
    {
        IEnumerable<MorphologyDTO> GetAllMorphologies();
        MorphologyDTO GetMorphology(int id);
        bool ExistsMorphology(string wrong_word);
        bool ExistsMorphologyId(int id);
        bool AddMorphology(MorphologyDTO morphDTO);
        bool EditMorphology(int id, MorphologyDTO morphDTO);
        bool DeleteMorphology(int id);
        void SaveChanges();
    }
}

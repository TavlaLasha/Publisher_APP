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
        IEnumerable<MorphologyDTO> GetMorphologies();
        MorphologyDTO GetMorphology(string id);
        bool AddMorphology(MorphologyDTO morphDTO);
        bool EditMorphology(string id, MorphologyDTO morphDTO);
        bool DeleteMorphology(string id);
        void SaveChanges();
    }
}

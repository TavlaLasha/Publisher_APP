using Models.DataViewModels.WordManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface IMorphologyManagement
    {
        FoundOccurrencesDTO FindMorphologies(string text);
        IEnumerable<MorphologyDTO> GetAllMorphologies();
        MorphologyDTO GetMorphology(string id);
        bool AddMorphology(MorphologyDTO morphDTO);
        bool EditMorphology(string id, MorphologyDTO morphDTO);
        bool DeleteMorphology(string id);
    }
}

using BLL.Contracts;
using DAL;
using Models.DataViewModels.WordManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.Services
{
    public class MorphologyManagement : IMorphologyManagement
    {
        public bool AddMorphology(MorphologyDTO morphDTO)
        {
            UnitOfWork _unitOfWork = new UnitOfWork(new DAL.EF.GeoHypDBContext());

            if (morphDTO == null || (morphDTO.Wrong_Word == string.Empty || morphDTO.Wrong_Word == null) || (morphDTO.Correct_Word == string.Empty || morphDTO.Correct_Word == null))
                throw new HttpException("No valid model given");

            if (_unitOfWork.MorphologyRepo.ExistsMorphology(morphDTO.Wrong_Word))
                throw new HttpException($"Morphology: {morphDTO.Wrong_Word} already exists in the database");

            _unitOfWork.MorphologyRepo.AddMorphology(morphDTO);
            _unitOfWork.MorphologyRepo.SaveChanges();
            return true;
        }

        public bool DeleteMorphology(string id)
        {
            UnitOfWork _unitOfWork = new UnitOfWork(new DAL.EF.GeoHypDBContext());

            if (id == string.Empty || Convert.ToInt32(id) < 1)
                throw new HttpException("No ID given");

            if (_unitOfWork.MorphologyRepo.ExistsMorphology(id))
                throw new HttpException($"Data with ID: {id} could not be found");

            _unitOfWork.MorphologyRepo.DeleteMorphology(Convert.ToInt32(id));
            _unitOfWork.MorphologyRepo.SaveChanges();
            return true;
        }

        public bool EditMorphology(string id, MorphologyDTO morphDTO)
        {
            UnitOfWork _unitOfWork = new UnitOfWork(new DAL.EF.GeoHypDBContext());

            if (morphDTO == null || (morphDTO.Wrong_Word == string.Empty || morphDTO.Wrong_Word == null) || (morphDTO.Correct_Word == string.Empty || morphDTO.Correct_Word == null))
                throw new HttpException("No valid model given");

            if (id == string.Empty || Convert.ToInt32(id) < 1)
                throw new HttpException("No ID given");

            int Id = Convert.ToInt32(id);
            if (!_unitOfWork.MorphologyRepo.ExistsMorphologyId(Id))
                throw new HttpException($"Data with ID: {Id} could not be found");

            _unitOfWork.MorphologyRepo.EditMorphology(Id, morphDTO);
            _unitOfWork.MorphologyRepo.SaveChanges();
            return true;
        }

        public MorphologyDTO GetMorphology(string id)
        {
            UnitOfWork _unitOfWork = new UnitOfWork(new DAL.EF.GeoHypDBContext());
            if (id == string.Empty || Convert.ToInt32(id) < 1)
                throw new HttpException("No ID given");

            int Id = Convert.ToInt32(id);
            if(!_unitOfWork.MorphologyRepo.ExistsMorphologyId(Id))
                throw new HttpException($"Data with ID: {Id} could not be found");

            return _unitOfWork.MorphologyRepo.GetMorphology(Id);
        }

        public IEnumerable<MorphologyDTO> GetAllMorphologies()
        {
            UnitOfWork _unitOfWork = new UnitOfWork(new DAL.EF.GeoHypDBContext());
            return _unitOfWork.MorphologyRepo.GetAllMorphologies();
        }
    }
}

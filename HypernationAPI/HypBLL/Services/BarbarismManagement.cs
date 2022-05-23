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
    public class BarbarismManagement : IBarbarismManagement
    {
        public bool AddBarbarism(BarbarismDTO brbDTO)
        {
            UnitOfWork _unitOfWork = new UnitOfWork(new DAL.EF.GeoHypDBContext());

            if (brbDTO == null || (brbDTO.Wrong_Word == string.Empty || brbDTO.Wrong_Word == null) || (brbDTO.Correct_Word == string.Empty || brbDTO.Correct_Word == null))
                throw new HttpException("No valid model given");

            if (_unitOfWork.BarbarismRepo.ExistsBarbarism(brbDTO.Wrong_Word))
                throw new HttpException($"Barbarism: {brbDTO.Wrong_Word} already exists in the database");

            _unitOfWork.BarbarismRepo.AddBarbarism(brbDTO);
            _unitOfWork.BarbarismRepo.SaveChanges();
            return true;
        }

        public bool DeleteBarbarism(string id)
        {
            UnitOfWork _unitOfWork = new UnitOfWork(new DAL.EF.GeoHypDBContext());

            if (id == string.Empty || Convert.ToInt32(id) < 1)
                throw new HttpException("No ID given");

            if (_unitOfWork.BarbarismRepo.ExistsBarbarism(id))
                throw new HttpException($"Data with ID: {id} could not be found");

            _unitOfWork.BarbarismRepo.DeleteBarbarism(Convert.ToInt32(id));
            _unitOfWork.BarbarismRepo.SaveChanges();
            return true;
        }

        public bool EditBarbarism(string id, BarbarismDTO brbDTO)
        {
            UnitOfWork _unitOfWork = new UnitOfWork(new DAL.EF.GeoHypDBContext());

            if (brbDTO == null || ((brbDTO.Wrong_Word == string.Empty || brbDTO.Wrong_Word == null) && (brbDTO.Correct_Word == string.Empty || brbDTO.Correct_Word == null)))
                throw new HttpException("No valid model given");

            if (id == string.Empty || Convert.ToInt32(id) < 1)
                throw new HttpException("No ID given");

            int Id = Convert.ToInt32(id);
            if (!_unitOfWork.BarbarismRepo.ExistsBarbarismId(Id))
                throw new HttpException($"Data with ID: {Id} could not be found");

            _unitOfWork.BarbarismRepo.EditBarbarism(Id, brbDTO);
            _unitOfWork.BarbarismRepo.SaveChanges();
            return true;
        }

        public BarbarismDTO GetBarbarism(string id)
        {
            UnitOfWork _unitOfWork = new UnitOfWork(new DAL.EF.GeoHypDBContext());
            if (id == string.Empty || Convert.ToInt32(id) < 1)
                throw new HttpException("No ID given");

            int Id = Convert.ToInt32(id);
            if (!_unitOfWork.BarbarismRepo.ExistsBarbarismId(Id))
                throw new HttpException($"Data with ID: {Id} could not be found");

            return _unitOfWork.BarbarismRepo.GetBarbarism(Id);
        }

        public IEnumerable<BarbarismDTO> GetAllBarbarisms()
        {
            UnitOfWork _unitOfWork = new UnitOfWork(new DAL.EF.GeoHypDBContext());
            return _unitOfWork.BarbarismRepo.GetAllBarbarisms();
        }
    }
}

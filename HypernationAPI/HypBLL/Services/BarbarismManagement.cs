using BLL.Contracts;
using DAL;
using Models.DataViewModels.DocManagement;
using Models.DataViewModels.WordManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace BLL.Services
{
    public class BarbarismManagement : IBarbarismManagement
    {
        public bool AddBarbarism(BarbarismDTO brbDTO)
        {
            UnitOfWork _unitOfWork = new UnitOfWork(new DAL.EF.GeoHypDBContext());

            if (brbDTO == null || (brbDTO.Wrong_Word == string.Empty || brbDTO.Wrong_Word == null))
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

        public FoundOccurrencesDTO FindBarbarisms(string text)
        {
            IEnumerable<BarbarismDTO> Barbarisms = GetAllBarbarisms();
            List<string> wrongs = Barbarisms.Select(i => i.Wrong_Word).ToList();
            FoundOccurrencesDTO fbdt = new FoundOccurrencesDTO();
            fbdt.occurrences = new Dictionary<string, int>();
            for (int i=0; i < wrongs.Count(); i++)
            {
                var pattern = $@"({wrongs[i]})";
                var regex = new Regex(pattern);
                int counter = 0;
                text = regex.Replace(text, delegate (Match m) 
                {
                    //int index = m.Index;
                    //int len = m.Length;
                    string correct = Barbarisms.Where(j => j.Wrong_Word == wrongs[i]).Select(j => j.Correct_Word).FirstOrDefault();
                    counter++;
                    string str = m.ToString();
                    return $"<span style='color:red' title='სწორია: {correct}'>{str}</span>";
                });
                if(counter>0)
                    fbdt.occurrences.Add(wrongs[i], counter);

                //Match match = Regex.Match(text, pattern);
                //if (match.Captures.Count > 0)
                //{
                //    string correct = Barbarisms.Where(j => j.Wrong_Word == wrongs[i]).Select(j => j.Correct_Word).FirstOrDefault();
                //    while (match.Success)
                //    {
                //        int index = match.Index;
                //        int len = match.Length;
                //        string replacement = $"<span style='color:red' title='{correct} {index}'>{wrongs[i]}</span>";
                //        text = Replace(text, index, len, replacement);

                //        match = match.NextMatch();
                //    }
                //}
            }
            fbdt.textDTO = new TextDTO() { Text = text };
            return fbdt;
        }

        public string Replace(string s, int index, int length, string replacement)
        {
            var builder = new StringBuilder();
            builder.Append(s.Substring(0, index));
            builder.Append(replacement);
            builder.Append(s.Substring(index + length));
            return builder.ToString();
        }
    }
}

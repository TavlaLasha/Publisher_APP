using DAL.EF;
using DAL.Repository.Contracts;
using Models.DataViewModels.WordManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class BarbarismRepository : IBarbarismRepository
    {
        private GeoHypDBContext _db;
        public BarbarismRepository(GeoHypDBContext db)
        {
            _db = db;
        }
        public bool AddBarbarism(BarbarismDTO brbDTO)
        {
            try
            {
                if (brbDTO == null)
                    return false;

                Barbarism brb = new Barbarism()
                {
                    id = brbDTO.Id,
                    wrong_word = brbDTO.Wrong_Word,
                    correct_word = brbDTO.Correct_Word,
                    decription = brbDTO.Description
                };
                _db.Barbarisms.Add(brb);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteBarbarism(int id)
        {
            try
            {
                var brb = _db.Barbarisms.Where(i => i.id == id).First();
                _db.Barbarisms.Remove(brb);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditBarbarism(int id, BarbarismDTO brbDTO)
        {
            try
            {
                var brb = _db.Barbarisms.Where(i => i.id == id).First();
                brb.wrong_word = brbDTO.Wrong_Word;
                brb.correct_word = brbDTO.Correct_Word;
                brb.decription = brbDTO.Description;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ExistsBarbarismId(int id)
        {
            return _db.Barbarisms.Any(i => i.id == id);
        }

        public bool ExistsBarbarism(string wrong_word)
        {
            return _db.Barbarisms.Any(i => i.wrong_word.Equals(wrong_word));
        }

        public BarbarismDTO GetBarbarism(int id)
        {
            return _db.Barbarisms.Where(i => i.id == id).Select(i => new BarbarismDTO
            {
                Id = i.id,
                Wrong_Word = i.wrong_word,
                Correct_Word = i.correct_word,
                Description = i.decription
            }).FirstOrDefault();
        }

        public IEnumerable<BarbarismDTO> GetAllBarbarisms()
        {
            return _db.Barbarisms.Select(i => new BarbarismDTO
            {
                Id = i.id,
                Wrong_Word = i.wrong_word,
                Correct_Word = i.correct_word,
                Description = i.decription
            });
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _db.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BarbarismRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}

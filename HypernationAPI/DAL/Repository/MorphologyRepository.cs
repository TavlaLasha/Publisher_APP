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
    public class MorphologyRepository : IMorphologyRepository
    {
        private GeoHypDBContext _db;
        public MorphologyRepository(GeoHypDBContext db)
        {
            _db = db;
        }
        public bool AddMorphology(MorphologyDTO morphDTO)
        {
            try
            {
                if (morphDTO == null)
                    return false;

                Morphology morph = new Morphology()
                {
                    id = morphDTO.Id,
                    wrong_word = morphDTO.Wrong_Word,
                    correct_word = morphDTO.Correct_Word,
                };
                _db.Morphologies.Add(morph);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteMorphology(int id)
        {
            try
            {
                var morph = _db.Morphologies.Where(i => i.id == id).First();
                _db.Morphologies.Remove(morph);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditMorphology(int id, MorphologyDTO morphDTO)
        {
            try
            {
                var morph = _db.Morphologies.Where(i => i.id == id).First();
                if (morphDTO.Wrong_Word != null)
                    morph.wrong_word = morphDTO.Wrong_Word;
                if (morphDTO.Correct_Word != null)
                    morph.correct_word = morphDTO.Correct_Word;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<MorphologyDTO> GetAllMorphologies()
        {
            return _db.Morphologies.Select(i => new MorphologyDTO
            {
                Id = i.id,
                Wrong_Word = i.wrong_word,
                Correct_Word = i.correct_word,
            });
        }

        public MorphologyDTO GetMorphology(int id)
        {
            return _db.Morphologies.Where(i => i.id == id).Select(i => new MorphologyDTO
            {
                Id = i.id,
                Wrong_Word = i.wrong_word,
                Correct_Word = i.correct_word,
            }).FirstOrDefault();
        }
        public bool ExistsMorphologyId(int id)
        {
            return _db.Morphologies.Any(i => i.id == id);
        }

        public bool ExistsMorphology(string wrong_word)
        {
            return _db.Morphologies.Any(i => i.wrong_word.Equals(wrong_word));
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
        // ~MorphologyRepository()
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

using DAL.EF;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork
    {
        private BarbarismRepository _brbRepo;
        private MorphologyRepository _morphRepo;

        private GeoHypDBContext _db;
        private DbContextTransaction dbContextTransaction;

        public UnitOfWork(GeoHypDBContext db)
        {
            _db = db;
        }
        public BarbarismRepository BarbarismRepo
        {
            get
            {
                if (_brbRepo == null)
                {
                    _brbRepo = new BarbarismRepository(_db);
                }
                return _brbRepo;
            }
        }
        public MorphologyRepository MorphologyRepo
        {
            get
            {
                if (_morphRepo == null)
                {
                    _morphRepo = new MorphologyRepository(_db);
                }
                return _morphRepo;
            }
        }

        public bool BeginTransaction()
        {
            bool jobDone = true;
            try
            {
                dbContextTransaction = _db.Database.BeginTransaction();
            }
            catch (Exception)
            {
                jobDone = false;
            }
            return jobDone;
        }
        public bool CommitTransaction()
        {
            bool jobDone = true;
            if (dbContextTransaction == null)
                throw new Exception("Transaction Not Started");

            try
            {
                dbContextTransaction.Commit();
            }
            catch (Exception)
            {
                jobDone = false;
                dbContextTransaction.Rollback();
            }
            dbContextTransaction.Dispose();
            return jobDone;
        }

        public bool Save()
        {
            bool jobDone = true;
            try
            {
                _db.SaveChanges();
            }
            catch (Exception)
            {
                jobDone = false;
            }
            return jobDone;
        }
    }
}

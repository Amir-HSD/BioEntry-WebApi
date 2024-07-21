using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Reflection;

namespace DataLayer
{
    public class FingerRepo : IFingerRepo
    {
        BioEntryContext _db;
        public FingerRepo(BioEntryContext db)
        {

            _db = db;

        }

        public IEnumerable<Finger> GetAllFinger()
        {
            return _db.Fingers.ToList();
        }

        public Finger GetFingerById(int id)
        {
            return _db.Fingers.FirstOrDefault(f=>f.Id == id);
        }

        public bool AddFinger(Finger finger)
        {
            try
            {
                _db.Fingers.Add(finger);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool UpdateFinger(Finger finger)
        {
            try
            {
                _db.Entry(finger).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool DeleteFinger(Finger finger)
        {
            try
            {
                _db.Entry(finger).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool DeleteFingerById(int id)
        {
            try
            {
                Finger finger = _db.Fingers.FirstOrDefault(f=>f.Id==id);
                _db.Entry(finger).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}

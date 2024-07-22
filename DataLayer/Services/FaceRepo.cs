using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class FaceRepo : IFaceRepo
    {
        BioEntryContext _db;
        public FaceRepo(BioEntryContext db)
        {

            _db = db;

        }

        public IEnumerable<Face> GetAllFace()
        {
            return _db.Faces.ToList();
        }

        public Face GetFaceById(int id)
        {
            return _db.Faces.Where(f=>f.Id == id).FirstOrDefault();
        }

        public bool AddFace(Face face)
        {
            try
            {
                _db.Faces.Add(face);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool UpdateFace(Face face)
        {
            try
            {
                _db.Entry(face).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool DeleteFace(Face face)
        {
            try
            {
                _db.Entry(face).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool DeleteFaceById(int id)
        {
            try
            {
                Face face = _db.Faces.Where(f=>f.Id == id).FirstOrDefault();
                _db.Entry(face).State = EntityState.Deleted;
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

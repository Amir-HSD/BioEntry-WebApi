using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer
{
    public class UserRepo : IUserRepo
    {
        BioEntryContext _db;
        public UserRepo(BioEntryContext db)
        {

            _db = db;

        }

        public IEnumerable<User> GetAllUser()
        {
            return _db.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _db.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public bool AddUser(User user)
        {
            try
            {
                _db.Users.Add(user);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                _db.Entry(user).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool DeleteUser(User user)
        {
            try
            {
                _db.Entry(user).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool DeleteUserById(int id)
        {
            User user = _db.Users.FirstOrDefault(u=> u.Id == id);
            try
            {
                _db.Entry(user).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public void SaveChange()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}

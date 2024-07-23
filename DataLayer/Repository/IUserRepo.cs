using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IUserRepo
    {
        IEnumerable<User> GetAllUser();

        User GetUserById(int id);

        bool AddUser(User user);

        bool UpdateUser(User user);

        bool DeleteUser(User user);

        bool DeleteUserById(int id);

        void SaveChange();

        void Dispose();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IFingerRepo
    {
        IEnumerable<Finger> GetAllFinger();

        Finger GetFingerById(int id);

        bool AddFinger(Finger finger);

        bool UpdateFinger(Finger finger);

        bool DeleteFinger(Finger finger);

        bool DeleteFingerById(int id);
    }
}

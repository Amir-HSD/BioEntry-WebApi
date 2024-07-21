using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IFaceRepo
    {
        IEnumerable<Face> GetAllFace();

        Face GetFaceById(int id);

        bool AddFace(Face face);

        bool UpdateFace(Face face);

        bool DeleteFace(Face face);

        bool DeleteFaceById(int id);
    }
}

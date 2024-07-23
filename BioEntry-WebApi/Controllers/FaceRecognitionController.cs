using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FaceRecognition;
using System.Threading.Tasks;

namespace BioEntry_WebApi.Controllers
{
    public class FaceRecognitionController : ApiController
    {
        BioEntryContext ctx = new BioEntryContext();
        IFaceRepo FaceRepo;
        IUserRepo UserRepo;

        public FaceRecognitionController()
        {
            FaceRepo = new FaceRepo(ctx);
            UserRepo = new UserRepo(ctx);
        }

        public async Task<IHttpActionResult> Get([FromBody]string Img)
        {
            CompareFace compareFace = new CompareFace();
            var result = await compareFace.Compare(Img, FaceRepo.GetAllFace().ToList());
            if (result == -1 || result == -2)
            {
                return Json(new {Result = "False"});
            }
            var UserFace = UserRepo.GetUserById(result);
            return Json(new {ID = UserFace.Id, Name = UserFace.Name, Family = UserFace.Family});
        }
    }
}

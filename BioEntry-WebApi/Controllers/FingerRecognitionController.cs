using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BioEntry_WebApi.Controllers
{
    public class FingerRecognitionController : ApiController
    {
        BioEntryContext ctx;
        IFingerRepo FingerRepo;
        IUserRepo UserRepo;
        public FingerRecognitionController()
        {
            ctx = new BioEntryContext();
            FingerRepo = new FingerRepo(ctx);
            UserRepo = new UserRepo(ctx);
        }

        public async Task<IHttpActionResult> Get([FromBody] int FingerId)
        {
            var Result = FingerRepo.GetFingerById(FingerId);
            if (Result == null)
            {
                return Json(new
                {
                    status = "Finger Not Found!"
                });
            }
            return Json(new { status = "Finger Successfully Founded", FingerId = Result.FingerId, UserId = Result.UserId, Name = Result.User.Name, Family = Result.User.Family });
        }

    }
}

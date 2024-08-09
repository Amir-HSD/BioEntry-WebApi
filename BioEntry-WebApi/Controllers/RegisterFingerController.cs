using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BioEntry_WebApi.Controllers
{
    public class RegisterFingerController : ApiController
    {
        BioEntryContext ctx = new BioEntryContext();
        IFingerRepo FingerRepo;
        public RegisterFingerController()
        {
            FingerRepo = new FingerRepo(ctx);
        }

        public IHttpActionResult Post([FromBody] RegisterFingerViewModel finger)
        {
            if (finger == null)
            {
                return Json(new { status = "Error!" });
            }
            FingerRepo.AddFinger(new Finger() { FingerId = finger.FingerId, UserId = finger.UserId });
            FingerRepo.SaveChange();
            return Json(new { status = "Finger Successfully Added" });
        }
    }
}

using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BioEntry_WebApi.Controllers
{
    public class RegisterFaceController : ApiController
    {
        BioEntryContext ctx = new BioEntryContext();
        IFaceRepo FaceRepo;
        public RegisterFaceController()
        {
            FaceRepo = new FaceRepo(ctx);
        }
        public IHttpActionResult Post([FromBody]FaceViewModel FaceViewModel)
        {
            if (FaceViewModel == null)
            {
                return Json(new { status = "Error! FaceViewModel is Null" });
            }
            Face face = new Face() { Img = FaceViewModel.Img, UserId = FaceViewModel.UserId };
            if (FaceRepo.AddFace(face))
            {
                FaceRepo.SaveChange();
                return Json(new { status = "Face has been successfully added" });
            }
            else
            {
                return Json(new { status= "Error!" });
            }
            
        }
    }
}

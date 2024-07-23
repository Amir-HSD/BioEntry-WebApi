using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BioEntry_WebApi.Controllers
{
    public class RegisterUserController : ApiController
    {
        BioEntryContext ctx = new BioEntryContext();
        IUserRepo UserRepo;
        public RegisterUserController()
        {
            UserRepo = new UserRepo(ctx);
        }

        public IHttpActionResult Post([FromBody]UserViewModel UserViewModel)
        {
            if (UserViewModel == null)
            {
                return Json(new { status = "Error! UserViewModel is Null" });
            }
            User user = new User() {
                Name = UserViewModel.Name,
                Family = UserViewModel.Family,
                BirthDate = UserViewModel.BirthDate,
                Email = UserViewModel.Email,
                PhoneNumber = UserViewModel.PhoneNumber
            };

            if (UserRepo.AddUser(user))
            {
                UserRepo.SaveChange();
                return Json(new { status = "User has been successfully added" });
            }
            else
            {
                return Json(new { status = "Error!" });
            }
        }
        public void Dispose()
        {
            UserRepo.Dispose();
        }
    }
}

using DataLayer;
using System.Diagnostics;
using System.Web.Http;
using System.Threading.Tasks;
using System;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;

namespace BioEntry_WebApi.Controllers
{
    public class FaceRecognitionController : ApiController
    {
        // Comparing two images directly in ASP.NET MVC .NET Framework is not feasible.
        // Using this method Post, we can compare two faces, but it slows down the process.
        // However, using the GET method can make this process faster, though it might pose some security risks.
        // All these issues can be resolved with ASP.NET Core.
        // Author AmirHSD

        // برای مقایسه دو تصویر دارای چهره در این فریم ورک به صورت مستقیم امکان نداره
        // با استفاده از متد پست کار ما راه میوفته اما سرعت کند هست
        // با استفاده از متد گت سرعت بالا تر میرود به این دلیل که پردازش به صورت مستقیم در برنامه انجام میشه
        // تمام این مشکلات در Asp.net core‌ وجود ندارد 
        // به زودی با Asp.net core ورژن جدید رو میزنم
        // توسعه دهنده Amir.HSD

        BioEntryContext ctx;
        IFaceRepo FaceRepo;
        IUserRepo UserRepo;

        public FaceRecognitionController()
        {
            ctx = new BioEntryContext();
            FaceRepo = new FaceRepo(ctx);
            UserRepo = new UserRepo(ctx);
        }

        public async Task<IHttpActionResult> Get()
        {
            List<Face> Faces = new List<Face>();
            foreach (var item in FaceRepo.GetAllFace().ToList())
            {
                Faces.Add(new Face
                {
                    Id = item.Id,
                    Img = item.Img,
                });
            }
            return Json(Faces);
        }

        [Route("api/FaceRecognition/{FaceId}")]
        public async Task<IHttpActionResult> Get(int FaceId)
        {
            var User = UserRepo.GetUserById(FaceId);
            if (User == null)
            {
                return Json(new { status = "Not Found" });
            }
            return Json(new
            {
                status = "Face Successfully Founded",
                ID = User.Id,
                Name = User.Name,
                Family = User.Family
            });
        }

        public async Task<IHttpActionResult> Post([FromBody]FaceRecognitionViewModel Img)
        {
            if (Img.Img == null || Img.Img == string.Empty)
            {
                return Json(new { status = "Error! Img is Empty" });
            }

            string FaceComparatorPath = ConfigurationManager.AppSettings["FaceComparatorPath"];
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = FaceComparatorPath,
                Arguments = $"{Img.Img}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                LoadUserProfile = true,
            };
            Process process = Process.Start(startInfo);
            process.WaitForExit();
            string ss = "";
            foreach (var item in await process.StandardOutput.ReadLineAsync())
            {
                ss += item.ToString();
            }

            string result = ss;
            

            if (Convert.ToInt32(result) == -1 || Convert.ToInt32(result) == -2)
            {
                return Json(new { status = "NotFound" });
            }

            var UserFace = UserRepo.GetUserById(Convert.ToInt32(result));

            return Json(new { status = "Face Successfully Compared", ID = UserFace.Id, Name = UserFace.Name, Family = UserFace.Family });

        }
    }
}

using System;
using System.Linq;
using System.Web.Http;
using prospekt.tel.Models;
using System.IO;

namespace prospekt.tel.Controllers.Api
{
    [Authorize]
    public class PersonPhotosController : ApiController
    {
        private psEnt db = new psEnt();

        public string Get(int id)
        {
            var imagePath = db.usp_GetPersonPhoto(id).FirstOrDefault();
            byte[] fileData = File.ReadAllBytes(imagePath);
            var base64 = Convert.ToBase64String(fileData);

            return "data:image/jpeg;base64," + base64;
        }
    }
}

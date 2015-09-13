using System;
using System.Linq;
using System.Web.Http;
using prospekt.tel.Models;
using System.IO;
using System.Web;

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

        [HttpPut]
        public IHttpActionResult Put(int id)
        {
            var person = db.usp_GetPersonById(id).FirstOrDefault();
            var filePartPath = System.Configuration.ConfigurationManager.AppSettings ["ScansPath"].ToString();
            var httpRequest = HttpContext.Current.Request;
            var uniqueKey = person.fam.ToString().Substring(0, 1) +
                            person.im.ToString().Substring(0, 1) +
                            person.ot.ToString().Substring(0, 1) +
                            person.passport_num.ToString() + @"\";
            var postedFile = httpRequest.Files ["photo"];
            if (!Directory.Exists(filePartPath + @"photos\" + uniqueKey))
            {
                Directory.CreateDirectory(filePartPath + @"photos\" + uniqueKey);
            }
            var photoFilePath = filePartPath + @"photos\" + uniqueKey + postedFile.FileName;
            postedFile.SaveAs(photoFilePath);
            try
            {
                var r = db.usp_Photos_Update(id, photoFilePath); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
            return Ok();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using prospekt.tel.Models;
using System.IO;
using System.Net.Http.Headers;
using System.Web;

namespace prospekt.tel.Controllers.Api
{
    [Authorize]
    public class PersonFullScansController : ApiController
    {
        private psEnt db = new psEnt();

        public HttpResponseMessage Get(int id)
        {
            var imagePath = db.usp_GetPersonPassScan(id).FirstOrDefault();
            byte[] fileData = File.ReadAllBytes(imagePath);
            var res = new HttpResponseMessage();
            res.Content = new ByteArrayContent(fileData);
            res.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            return res;
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
            var postedScanFile = httpRequest.Files ["scan"];
            if (!Directory.Exists(filePartPath + @"scans\" + uniqueKey))
            {
                Directory.CreateDirectory(filePartPath + @"scans\" + uniqueKey);
            }
            var scanFilePath = filePartPath + @"scans\" + uniqueKey + postedScanFile.FileName;
            postedScanFile.SaveAs(scanFilePath);
            try
            {
                var r = db.usp_Scans_Update(id, scanFilePath);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
            return Ok();
        }
    }
}

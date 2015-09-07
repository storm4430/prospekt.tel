using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using prospekt.tel.Models;
using Microsoft.Ajax.Utilities;
using System.IO;
using System.Web;

namespace prospekt.tel.Controllers.Api
{
    [Authorize]
    public class PersonsController : ApiController
    {
        private psEnt db = new psEnt();

        public IHttpActionResult Get()
        {
            try
            {
                var result = db.usp_GetPersonFIOLetters().ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
                throw;
            }
        }

        public IHttpActionResult Get(string substr)
        {
            try
            {
                var result = "Жарков денис"; //db.usp_GetPersonFIOLetters().ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult Create()
        {
            var filePartPath = System.Configuration.ConfigurationManager.AppSettings ["ScansPath"].ToString();
            var httpRequest = HttpContext.Current.Request;
            var uniqueKey = httpRequest.Form ["fam"].ToString().Substring(0,1) +
                            httpRequest.Form ["im"].ToString().Substring(0, 1) +
                            httpRequest.Form ["ot"].ToString().Substring(0, 1) +
                            httpRequest.Form ["doc_num"].ToString() +@"\";
            var postedFile = httpRequest.Files ["photo"];
            if (!Directory.Exists(filePartPath + @"photos\" + uniqueKey))
            {
                Directory.CreateDirectory(filePartPath + @"photos\" + uniqueKey);
            }
            var photoFilePath = filePartPath + @"photos\" + uniqueKey + postedFile.FileName;
            postedFile.SaveAs(photoFilePath);
            var postedScanFile = httpRequest.Files ["scan"];
            if (!Directory.Exists(filePartPath + @"scans\" + uniqueKey))
            {
                Directory.CreateDirectory(filePartPath + @"scans\" + uniqueKey);
            }
            var scanFilePath = filePartPath + @"scans\" + uniqueKey + postedScanFile.FileName;
            postedScanFile.SaveAs(scanFilePath);
            usp_GetPerson_Result obj = new usp_GetPerson_Result()
            {
                fam = httpRequest.Form ["fam"].ToString(),
                im = httpRequest.Form ["im"].ToString(),
                ot = httpRequest.Form ["ot"].ToString(),
                sex = Convert.ToInt32(httpRequest.Form ["sex"].ToString()),
                birthday = DateTime.Parse(httpRequest.Form ["dr"].ToString()),
                passport_serie = httpRequest.Form ["doc_ser"].ToString(),
                passport_num = httpRequest.Form ["doc_num"].ToString(),
                pers_photo = photoFilePath,
                pass_scan = scanFilePath,
                cellPhone = httpRequest.Form ["cellPhone"].ToString(),
                person_comment = httpRequest.Form ["comment"].ToString()
            };
            
            try
            {
                var result = db.usp_Persons_IU(null, obj.fam, obj.im, obj.ot, obj.sex, obj.birthday, obj.person_comment, obj.pers_photo, obj.pass_scan, obj.passport_serie, obj.passport_num, obj.cellPhone);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
                throw;
            }
        }
    }

    //public partial class usp_GetPerson_Result
    //{
    //    public File passport_scan {  };
    //    public File photo { };
    //}
}

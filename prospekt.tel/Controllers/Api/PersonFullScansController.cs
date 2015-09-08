using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using prospekt.tel.Models;
using System.IO;
using System.Net.Http.Headers;

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
    }
}

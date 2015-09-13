using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using prospekt.tel.Models;
using System.IO;

namespace prospekt.tel.Controllers.Api
{
    [Authorize]
    public class PersonScansController : ApiController
    {
        private psEnt db = new psEnt();

        public string Get(int id)
        {
            var result = db.usp_GetPersonPassScan(id).FirstOrDefault();
            return Path.GetFileName(result);
        }
    }
}

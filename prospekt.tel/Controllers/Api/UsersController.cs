using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using prospekt.tel.Models;


namespace prospekt.tel.Controllers.Api
{
    [Authorize]
    public class UserController : ApiController
    {
        private psEnt db = new psEnt();

        public IHttpActionResult Get(string substr)
        {
            try
            {
                var result = db.usp_GetAllUsers(substr).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
    }
}

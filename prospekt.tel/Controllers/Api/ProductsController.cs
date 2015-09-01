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
    public class ProductsController : ApiController
    {
        private psEnt db = new psEnt();

        public IHttpActionResult Get(int id, string substr = "")
        {
            try
            {
                var result = db.usp_GetAllProducts(id, substr).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
    }
}

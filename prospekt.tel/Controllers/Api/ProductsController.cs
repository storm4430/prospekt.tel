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

        [HttpPost]
        public IHttpActionResult Get(usp_GetAllProducts_Result obj)
        {
            try
            {
                var result = db.usp_Products_IU(null,obj.catId, obj.subcatId, obj.product_name, Common.DataHelper.GetUserRealName(User.Identity.Name));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using prospekt.tel.Models;
using prospekt.tel.Common;

namespace prospekt.tel.Controllers.Api
{
    [Authorize]
    public class ProductsController : ApiController
    {
        private psEnt db = new psEnt();

        public IHttpActionResult Get(string substr)
        {
            try
            {
                var result = db.usp_GetAllProducts_sel(substr).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        public IHttpActionResult Get(int id, string substr)
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

        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = db.usp_GetProductById(id).FirstOrDefault();
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
                var result = db.usp_Products_IU(null,obj.catId, obj.subcatId, obj.product_name, DataHelper.GetUserRealName(User.Identity.Name));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        // PUT: api/products/5
        [HttpPut]
        public IHttpActionResult Put(int id, usp_GetProductById_Result objData)
        {
            try
            {
                var result = db.usp_Products_IU(id, null, null, objData.product_name , DataHelper.GetUserRealName(User.Identity.Name));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        // DELETE: api/products/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = db.usp_Products_Delete(id, DataHelper.GetUserRealName(User.Identity.Name));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
    }
}

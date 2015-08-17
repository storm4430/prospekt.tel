using System.Web.Http;
using prospekt.tel.Models;
using System.Linq;
using System;
using prospekt.tel.Common;
using System.Web;
using System.Threading.Tasks;

namespace prospekt.tel.Controllers.Api
{
    [Authorize]
    public class SubCategoriesController : ApiController
    {
        private psEnt db = new psEnt();
        // GET: api/Categories
        public IHttpActionResult Get(int catid, string substr)
        {
            try
            {
                var result = db.usp_GetAllSubCategoriesByCategoryID(catid, substr).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

        }

        // GET: api/Categories/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = db.usp_GetCategoryById(id).FirstOrDefault();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        // POST: api/Categories
        public IHttpActionResult Post(usp_GetAllCategories_Result objData)
        {
            try
            {
                var result = db.usp_Ctegories_IU(null, objData.category_desc, DataHelper.GetUserRealName(HttpContext.Current.User.Identity.Name));
                return  Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        // PUT: api/Categories/5
        public IHttpActionResult Put(int id, usp_GetAllCategories_Result objData)
        {
            try
            {
                var result = db.usp_Ctegories_IU(id, objData.category_desc, DataHelper.GetUserRealName(HttpContext.Current.User.Identity.Name));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        // DELETE: api/Categories/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = db.usp_Ctegories_Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
    }


}

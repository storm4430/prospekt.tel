using System;
using System.Linq;
using System.Web.Http;
using prospekt.tel.Models;

namespace prospekt.tel.Controllers.Api
{
    [Authorize]
    public class OrgsController : ApiController
    {
        private psEnt db = new psEnt();

        public IHttpActionResult Get(string substr)
        {
            try
            {
                var result = db.usp_GetAllOrgs(substr).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult Insert(usp_GetAllOrgs_Result obj)
        {
            try
            {
                var result = db.usp_Orgs_IU(null, obj.orgName, obj.orgINN, obj.orgOGRN, obj.orgAdress, obj.orgRukFam, obj.orgRukIm, obj.orgRukOt);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
                throw;
            }
        }
    }
}

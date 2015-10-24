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
    public class ContractsController : ApiController
    {
        private psEnt db = new psEnt();

        public IHttpActionResult Get(string substr)
        {
            Guid userOrg = Common.DataHelper.GetUserOrg(User.Identity.Name);
            try
            {
                if (User.IsInRole("Администратор"))
                {
                    var result = db.usp_GetAllContracts(null, substr).ToList();
                    return Ok(result);
                }
                else
                {
                    var result = db.usp_GetAllContracts(userOrg, substr).ToList();
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
    }
}

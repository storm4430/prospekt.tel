using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prospekt.tel.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class OrgController : Controller
    {
        // GET: Org
        public ActionResult Index()
        {
            return PartialView();
        }

        // GET: Insert
        public ActionResult Insert()
        {
            return PartialView();
        }
    }
}
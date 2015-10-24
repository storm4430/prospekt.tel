using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prospekt.tel.Models;

namespace prospekt.tel.Controllers
{
    [Authorize]
    public class ContractController : Controller
    {
        private psEnt db = new psEnt();
        // GET: Contract
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult Insert()
        {
            return PartialView();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prospekt.tel.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Index()
        {
            return PartialView();
        }

        // GET: Create
        public ActionResult Create()
        {
            return PartialView();
        }
    }
}
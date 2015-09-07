using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prospekt.tel.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        // GET: Category
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            ViewBag.SCId = id;
            return PartialView();
        }

        // GET: Category/Insert
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Insert()
        {
            return PartialView();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.ProductId = id;
            return PartialView();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            ViewBag.ProductId = id;
            return PartialView();
        }
    }
}
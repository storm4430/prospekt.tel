using System.Web.Mvc;

namespace prospekt.tel.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        // GET: Category
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
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
            ViewBag.CategoryId = id;
            return PartialView();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            ViewBag.CategoryId = id;
            return PartialView();
        }
    }
}
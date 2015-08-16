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
        public ActionResult Details(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            return View();
        }
    }
}
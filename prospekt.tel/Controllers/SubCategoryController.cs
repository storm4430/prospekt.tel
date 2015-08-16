using System.Web.Mvc;

namespace prospekt.tel.Controllers
{
    [Authorize]
    public class SubCategoryController : Controller
    {
        // GET: SubCategory
        /// <summary>
        /// Возвращает представление для подкатегории товара
        /// </summary>
        /// <param name="categoryId">Идентификатор категории товара</param>
        /// <returns>SubCategory/Index.cshtml</returns>
        public ActionResult Index(int categoryId)
        {
            ViewBag.categoruId = categoryId;
            return View();
        }
    }
}
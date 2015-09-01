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
            ViewBag.categoryId = categoryId;
            return PartialView();
        }

        public ActionResult Insert(int id)
        {
            ViewBag.CatId = id;
            return PartialView();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.CatId = id;
            return PartialView();
        }

        public ActionResult Delete(int id)
        {
            ViewBag.subCatId = id;
            return PartialView();
        }
    }
}
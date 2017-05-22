using System.Web.Mvc;

namespace RawSharer.Controllers.UI
{
    public sealed class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Base()
        {
            return View();
        }
    }
}
using System.Web.Mvc;

namespace KmandiliWebService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.Title = "Home Page";

            return new HttpNotFoundResult();
        }
    }
}

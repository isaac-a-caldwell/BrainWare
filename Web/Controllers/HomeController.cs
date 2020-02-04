namespace Web.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //assuming test-driven development, existing test expects resulting ViewBag to have Title
            var viewResult = View();
            viewResult.ViewData["Title"] = "Home Page";

            return viewResult;
        }


    }
}

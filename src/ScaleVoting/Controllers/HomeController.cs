using System.Web.Mvc;

namespace ScaleVoting.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return Redirect("/ControlPanel");
            }

            return View();
        }
    }
}
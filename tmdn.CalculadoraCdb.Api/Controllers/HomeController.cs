using System.Web.Mvc;

namespace tmdn.CalculadoraCdb.Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace MyApp.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class TopicsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

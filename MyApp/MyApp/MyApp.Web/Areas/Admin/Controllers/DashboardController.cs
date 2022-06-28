using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin, Manager")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

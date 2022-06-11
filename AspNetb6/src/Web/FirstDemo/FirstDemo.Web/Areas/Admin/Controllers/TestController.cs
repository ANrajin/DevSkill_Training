using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestController : Controller
    {
        [Authorize(Policy = "TestRequirementPolicy")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestController : Controller
    {
        [Authorize(Policy = "MerchantPolicy")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "TestViewPolicy")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "TestRequirementPolicy")]
        public IActionResult Update()
        {
            return View();
        }
    }
}

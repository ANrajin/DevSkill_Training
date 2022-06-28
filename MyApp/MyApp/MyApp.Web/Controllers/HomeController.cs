using Autofac; 
using Microsoft.AspNetCore.Mvc;
using MyApp.Web.Models;
using System.Diagnostics;

namespace MyApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ILifetimeScope _scope;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ILifetimeScope scope)
        {
            _logger = logger;
            _configuration = configuration;
            _scope = scope;
        }

        public IActionResult Test()
        {
            var model = _scope.Resolve<CourseViewModel>();
            model.CreateCourse("React", 30000);
            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
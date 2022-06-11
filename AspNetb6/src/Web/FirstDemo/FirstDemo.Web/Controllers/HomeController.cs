using Autofac;
using FirstDemo.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FirstDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ILifetimeScope _scope;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration,
            ILifetimeScope scope)
        {
            _logger = logger;
            _configuration = configuration;
            _scope = scope;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Currently In Home Index");
            var model = new IndexModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(IndexModel model)
        {
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Test()
        {
            var model = new TestModel();
            model.InsertData(_configuration.GetConnectionString("DefaultConnection"));
            return View(model);
        }

        public IActionResult Test2()
        {
            var model = new TestModel();
            model.GetData(_configuration.GetConnectionString("DefaultConnection"));
            return View(model);
        }

        public IActionResult Test3()
        {
            var model = new TestModel();
            model.GetDataUsingDataSet(_configuration.GetConnectionString("DefaultConnection"));
            return View(model);
        }

        public IActionResult Test4()
        {
            var model = _scope.Resolve<CourseModel>();
            model.CreateCourse("Asp.net", 30000);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Test(TestModel model)
        {
            if(ModelState.IsValid)
            {
                model.DoSomething();
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
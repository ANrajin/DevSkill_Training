using Autofac;
using Microsoft.AspNetCore.Mvc;
using MyApp.Web.Areas.Admin.Models;

namespace MyApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EnrollsController : Controller
    {
        private readonly ILogger<EnrollsController> _logger;
        private readonly ILifetimeScope _scope;

        public EnrollsController(ILifetimeScope scope, ILogger<EnrollsController> logger)
        {
            _logger = logger;
            _scope = scope;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var model = _scope.Resolve<EnrollmentCreateModel>();
            model.LoadData();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(EnrollmentCreateModel model)
        {
            if (ModelState.IsValid)
            {
                model.Resolve(_scope);

                try
                {
                    model.Enroll();

                    TempData["ResponseType"] = "success";
                    TempData["ResponseMessage"] = "Successfully enrolled a new student";
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData["ResponseType"] = "danger";
                    TempData["ResponseMessage"] = "Internal server error!";
                }
            }
            return RedirectToAction("Create", model);
        }
    }
}

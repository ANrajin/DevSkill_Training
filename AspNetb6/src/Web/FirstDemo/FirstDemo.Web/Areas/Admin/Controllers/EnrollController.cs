using Autofac;
using FirstDemo.Web.Areas.Admin.Models;
using FirstDemo.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EnrollController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<EnrollController> _logger;

        public EnrollController(ILogger<EnrollController> logger, ILifetimeScope scope)
        {
            _scope = scope;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewEnrollment()
        {
            var model = _scope.Resolve<EnrollmentCreateModel>();
            model.LoadData();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult NewEnrollment(EnrollmentCreateModel model)
        {
           if(ModelState.IsValid)
            {
                model.Resolve(_scope);

                try
                {
                    model.LoadData();
                    model.Enroll();
                    TempData["ResponseMessage"] = "Successfuly enrolled student.";
                    TempData["ResponseType"] = ResponseTypes.Success;
                }
                catch (Exception ex)
                {
                    TempData["ResponseMessage"] = "Failed to enroll student.";
                    TempData["ResponseType"] = ResponseTypes.Danger;

                    _logger.LogError(ex, "Failed to enroll student");
                }
            }
            return View(model);
        }
    }
}

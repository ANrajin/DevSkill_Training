using Autofac;
using Microsoft.AspNetCore.Mvc;
using MyApp.Training.Exceptions;
using MyApp.Web.Areas.Admin.Models;
using MyApp.Web.Models;

namespace MyApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentsController : Controller
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly ILifetimeScope _scope;

        public StudentsController(ILogger<StudentsController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetStudents()
        {
            DatatableAjaxRequestModel dataTableModel = new DatatableAjaxRequestModel(Request);
            var model = _scope.Resolve<StudentListModel>();

            return Json(model.GetPaginatedStudents(dataTableModel));
        }

        public IActionResult Create()
        {
            var model = _scope.Resolve<StudentCreateModel>();
            return View(model);
        }

        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult Create(StudentCreateModel model)
        {
            if (ModelState.IsValid)
            {
                model.Resolve(_scope);

                try
                {
                    model.Create();
                    TempData["ResponseType"] = "success";
                    TempData["ResponseMessage"] = "Successfully registered a new student.";
                    return RedirectToAction("Index");
                }
                catch (DuplicateException ioe)
                {
                    _logger.LogError(ioe, ioe.Message);
                    TempData["ResponseType"] = "danger";
                    TempData["ResponseMessage"] = ioe.Message;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData["ResponseType"] = "danger";
                    TempData["ResponseMessage"] = "Internal server error!";
                }
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = _scope.Resolve<StudentEditModel>();
            model.LoadData(id);
            return View(model);
        }

        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult Edit(StudentEditModel model)
        {
            if (ModelState.IsValid)
            {
                model.Resolve(_scope);

                try
                {
                    model.Update();
                    TempData["ResponseType"] = "success";
                    TempData["ResponseMessage"] = "Successfully updated the student.";
                    return RedirectToAction("Index");
                }
                catch(DuplicateException ioe)
                {
                    _logger.LogError(ioe, ioe.Message);
                    TempData["ResponseType"] = "danger";
                    TempData["ResponseMessage"] = ioe.Message;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData["ResponseType"] = "danger";
                    TempData["ResponseMessage"] = "Internal server error!";
                }
            }

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var model = _scope.Resolve<StudentListModel>();
                model.Delete(id);

                TempData["ResponseType"] = "success";
                TempData["ResponseMessage"] = "Successfully deleted the course.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                TempData["ResponseType"] = "danger";
                TempData["ResponseMessage"] = "Internal server error!";
            }
            return RedirectToAction("Index");
        }
    }
}

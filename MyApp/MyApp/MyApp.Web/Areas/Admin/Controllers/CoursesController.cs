using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Training.Exceptions;
using MyApp.Web.Areas.Admin.Models;
using MyApp.Web.Models;
using MyApp.Web.Utilities;

namespace MyApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Manager")]
    public class CoursesController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ILifetimeScope scope, ILogger<CoursesController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetCourses()
        {
            DatatableAjaxRequestModel dataTableModel = new DatatableAjaxRequestModel(Request);
            var model = _scope.Resolve<CourseListModel>();

            return Json(model.GetPaginatedCourses(dataTableModel));
        }

        public IActionResult Create()
        {
            var model = _scope.Resolve<CourseCreateModel>();
            return View(model);
        }

        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult Create(CourseCreateModel model)
        {
            if (ModelState.IsValid)
            {
                model.Resolve(_scope);
                try
                {
                    model.Create();
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully added a new course.",
                        Type = ResponseType.Success
                    });
                    return RedirectToAction("Index");
                }
                catch (DuplicateException ioe)
                {
                    _logger.LogError(ioe, ioe.Message);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = ioe.Message,
                        Type = ResponseType.Danger
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Internal server error!",
                        Type = ResponseType.Danger
                    });
                }
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = _scope.Resolve<CourseEditModel>();
            model.LoadData(id);
            return View(model);
        }

        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult Edit(CourseEditModel model)
        {
            if (ModelState.IsValid)
            {
                model.Resolve(_scope);
                try
                {
                    model.Update();
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Successfully updated the course.",
                        Type = ResponseType.Success
                    });
                    return RedirectToAction("Index");
                }
                catch (DuplicateException ioe)
                {
                    _logger.LogError(ioe, ioe.Message);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = ioe.Message,
                        Type = ResponseType.Danger
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "Internal server error!",
                        Type = ResponseType.Danger
                    });
                }
            }

            return RedirectToAction("Edit", model);
        }

        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var model = _scope.Resolve<CourseListModel>();
                model.DeleteCourse(id);

                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Successfully deleted the course.",
                    Type = ResponseType.Success
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Internal server error!",
                    Type = ResponseType.Danger
                });
            }
            return RedirectToAction("Index");
        }
    }
}

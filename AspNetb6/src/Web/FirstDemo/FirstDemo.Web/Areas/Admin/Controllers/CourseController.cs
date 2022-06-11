using Autofac;
using FirstDemo.Training.Exceptions;
using FirstDemo.Web.Areas.Admin.Models;
using FirstDemo.Web.Models;
using Microsoft.AspNetCore.Mvc;
using FirstDemo.Web.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace FirstDemo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin,Manager")]
    public class CourseController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<CourseController> _logger;

        public CourseController(ILogger<CourseController> logger, ILifetimeScope scope)
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
            var dataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<CourseListModel>();
            return Json(model.GetPagedCourses(dataTableModel));
        }

        public IActionResult Create()
        {
            var model = _scope.Resolve<CourseCreateModel>();
            return View(model);
        }

        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult Create(CourseCreateModel model)
        {
            if(ModelState.IsValid)
            {
                model.Resolve(_scope);

                try
                {
                    model.CreateCourse();
                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel 
                    { 
                        Message = "Successfully added a new course.", 
                        Type = ResponseTypes.Success 
                    });

                    return RedirectToAction("Index");
                }
                catch(DuplicateException ioe)
                {
                    _logger.LogError(ioe, ioe.Message);

                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = ioe.Message,
                        Type = ResponseTypes.Danger
                    });
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, ex.Message);

                    TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                    {
                        Message = "There was a problem in creating course.",
                        Type = ResponseTypes.Danger
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
                    model.EditCourse();

                    TempData["ResponseMessage"] = "Successfuly updated course.";
                    TempData["ResponseType"] = ResponseTypes.Success;

                    return RedirectToAction("Index");
                }
                catch (DuplicateException ioe)
                {
                    _logger.LogError(ioe, ioe.Message);

                    TempData["ResponseMessage"] = ioe.Message;
                    TempData["ResponseType"] = ResponseTypes.Danger;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    TempData["ResponseMessage"] = "There was a problem in updating course.";
                    TempData["ResponseType"] = ResponseTypes.Danger;
                }
            }

            return View(model);
        }

        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var model = _scope.Resolve<CourseListModel>();
                model.DeleteCourse(id);

                TempData["ResponseMessage"] = "Successfuly deleted course.";
                TempData["ResponseType"] = ResponseTypes.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                TempData["ResponseMessage"] = "There was a problem in deleteing course.";
                TempData["ResponseType"] = ResponseTypes.Danger;
            }

            return RedirectToAction("Index");
        }
    }
}

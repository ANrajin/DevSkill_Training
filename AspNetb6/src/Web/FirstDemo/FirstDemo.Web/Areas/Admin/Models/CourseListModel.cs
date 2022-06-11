using FirstDemo.Training.Services;
using FirstDemo.Web.Models;

namespace FirstDemo.Web.Areas.Admin.Models
{
    public class CourseListModel
    {
        private readonly ICourseService _courseService;

        public CourseListModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public object GetPagedCourses(DataTablesAjaxRequestModel model)
        {
            var data = _courseService.GetCourses(
                model.PageIndex,
                model.PageSize,
                model.SearchText,
                model.GetSortText(new string[] { "Name", "Fee" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Fees.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void DeleteCourse(int id)
        {
            _courseService.DeleteCourse(id);
        }
    }
}

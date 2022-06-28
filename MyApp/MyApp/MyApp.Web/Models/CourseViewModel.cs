using MyApp.Training.BusinessObjects;
using MyApp.Training.Services;

namespace MyApp.Web.Models
{
    public class CourseViewModel
    {
        private readonly ICourseService _courseService;

        public CourseViewModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        internal void CreateCourse(string name, double fee)
        {
            var course = new Course
            {
                Name = name,
                Fees = fee,
            };

            _courseService.CreateCourse(course);
        }
    }
}

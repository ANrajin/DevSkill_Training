using FirstDemo.Training.BusinessObjects;
using FirstDemo.Training.DbContexts;
using FirstDemo.Training.Services;

namespace FirstDemo.Web.Models
{
    public class CourseModel
    {
        private readonly ICourseService _courseService;

        public CourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        internal void CreateCourse(string name, int fee)
        {
            var course = new Course
            {
                Name = name,
                Fees = fee
            };
            _courseService.CreateCourse(course);
        }
    }
}

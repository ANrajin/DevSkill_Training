using Autofac;
using AutoMapper;
using FirstDemo.Training.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirstDemo.Web.Areas.Admin.Models
{
    public class EnrollmentCreateModel
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public IList<SelectListItem> Courses { get; set; }
        public IList<SelectListItem> Students { get; set; }

        private ICourseEnrollmentService _courseEnrollmentService;
        private ICourseService _courseService;
        private IStudentService _studentService;
        private ILifetimeScope _scope;
        private IMapper _mapper;

        public EnrollmentCreateModel()
        {
            Courses = new List<SelectListItem>();
            Students = new List<SelectListItem>();
        }

        public EnrollmentCreateModel(ICourseEnrollmentService courseEnrollmentService,
            ICourseService courseService, IStudentService studentService)
            : this()
        {
            _courseEnrollmentService = courseEnrollmentService;
            _courseService = courseService;
            _studentService = studentService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _courseEnrollmentService = _scope.Resolve<ICourseEnrollmentService>();
            _courseService = _scope.Resolve<ICourseService>(); ;
            _studentService = _scope.Resolve<IStudentService>(); ;
            _mapper = _scope.Resolve<IMapper>();
        }

        public void LoadData()
        {
            var courses = _courseService.GetCourses();
            var students = _studentService.GetStudents();

            foreach (var course in courses)
            {
                Courses.Add(new SelectListItem { Text = course.Name, 
                    Value = course.Id.ToString() });
            }

            Students = (from s in students
                        select new SelectListItem { Text = s.Name, 
                            Value = s.Id.ToString() }).ToList();
        }

        public void Enroll()
        {
            var course = _courseService.GetCourse(CourseId);
            var student = _studentService.GetStudent(StudentId);

            _courseEnrollmentService.EnrollStudent(course, student);
        }
    }
}

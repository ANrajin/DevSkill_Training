using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyApp.Training.Services;

namespace MyApp.Web.Areas.Admin.Models
{
    public class EnrollmentCreateModel
    {

        public int CourseId { get; set; }
        public int StudentID { get; set; }
        /*
         * @Microsoft.AspNetCore.Mvc.Rendering
         * 
         * <SelectListItem> is used for showing Dropdown
         * Hold Data as Name, Value Pair
         *
         **/
        public IList<SelectListItem> Courses { get; set; }
        public IList<SelectListItem> Students { get; set; }

        private ICourseEnrollmentService _service;
        private IMapper _mapper;
        private ILifetimeScope _scope;
        private ICourseService _courseService;
        private IStudentService _studentService;

        public EnrollmentCreateModel()
        {
            Courses = new List<SelectListItem>();
            Students = new List<SelectListItem>();
        }

        public EnrollmentCreateModel(
            ICourseEnrollmentService courseEnrollmentService,
            ICourseService courseService,
            IStudentService studentService
            ):this()
        {
            _service = courseEnrollmentService;
            _courseService = courseService;
            _studentService = studentService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _service = _scope.Resolve<ICourseEnrollmentService>();
            _mapper = _scope.Resolve<IMapper>();
            _courseService = _scope.Resolve<ICourseService>();
            _studentService = _scope.Resolve<IStudentService>();
        }

        public void LoadData()
        {
            var courses = _courseService.GetAllCourses();
            var students = _studentService.GetAllStudents();

            foreach (var course in courses)
            {
                Courses.Add(new SelectListItem
                {
                    Text = course.Name,
                    Value = course.Id.ToString()
                });
            }

            Students = (from student in students
                       select new SelectListItem { 
                           Text = student.Name, Value = student.Id.ToString() }).ToList();
        }

        public void Enroll()
        {
            var course = _courseService.GetCourse(CourseId);
            var student = _studentService.GetStudent(StudentID);

            _service.EnrollStudent(course, student);
        }
    }
}

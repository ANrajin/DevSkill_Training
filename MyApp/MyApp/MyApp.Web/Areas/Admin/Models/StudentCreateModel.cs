using Autofac;
using AutoMapper;
using MyApp.Training.BusinessObjects;
using MyApp.Training.Services;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Web.Areas.Admin.Models
{
    public class StudentCreateModel
    {
        private IMapper _mapper;
        private IStudentService _studentService;
        private ILifetimeScope _scope;

        [Required, StringLength (100, ErrorMessage ="Name should be less than 100 char")]
        public string? Name { get; set; }
        [Required, Range (0,5, ErrorMessage = "CGPA should be between 0 and 5")]
        public double? CGPA { get; set; }
        public string? Address { get; set; }

        public StudentCreateModel()
        {

        }

        public StudentCreateModel(IMapper mapper, IStudentService studentService)
        {
            _mapper = mapper;
            _studentService = studentService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _studentService = _scope.Resolve<IStudentService>();
            _mapper = _scope.Resolve<IMapper>();
        }

        internal void Create()
        {
            var student = _mapper.Map<Student>(this);
            _studentService.CreateStudent(student);
        }
    }
}

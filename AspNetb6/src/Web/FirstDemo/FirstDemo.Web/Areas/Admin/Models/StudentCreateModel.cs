using Autofac;
using AutoMapper;
using FirstDemo.Training.BusinessObjects;
using FirstDemo.Training.Services;
using System.ComponentModel.DataAnnotations;

namespace FirstDemo.Web.Areas.Admin.Models
{
    public class StudentCreateModel
    {
        private IStudentService _studentService;
        private ILifetimeScope _scope;
        private IMapper _mapper;

        [StringLength(100, ErrorMessage = "Name should be less than 100 chars")]
        public string Name { get; set; }
        [Range(0, 5, ErrorMessage = "CGPA should be between 0 to 5")]
        public double CGPA { get; set; }
        public string Address { get; set; }

        public StudentCreateModel()
        {

        }

        public StudentCreateModel(IMapper mapper, IStudentService studentService)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _studentService = _scope.Resolve<IStudentService>();
            _mapper = _scope.Resolve<IMapper>();
        }

        internal void CreateStudent()
        {
            var student = _mapper.Map<Student>(this);

            _studentService.CreateStudent(student);
        }
    }
}

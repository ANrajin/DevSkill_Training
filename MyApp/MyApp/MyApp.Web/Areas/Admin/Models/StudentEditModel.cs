using Autofac;
using AutoMapper;
using MyApp.Training.BusinessObjects;
using MyApp.Training.Services;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Web.Areas.Admin.Models
{
    public class StudentEditModel
    {
        private IStudentService _service;
        private IMapper _mapper;
        private ILifetimeScope _scope;

        public int Id { get; set; }
        [Required, StringLength(100, ErrorMessage = "The length of name must be 255")]
        public string? Name { get; set; }
        [Range(0,5, ErrorMessage = "CGPA should be between 0 and 5")]
        public double? CGPA { get; set; }
        public string? Address { get; set; }

        public StudentEditModel()
        {

        }

        public StudentEditModel(IMapper mapper, IStudentService service)
        {
            _mapper = mapper;
            _service = service;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _service = _scope.Resolve<IStudentService>();
            _mapper = _scope.Resolve<IMapper>();
        }

        internal void LoadData(int id)
        {
            var student = _service.GetStudent(id);
            _mapper.Map(student, this);
        }

        internal void Update()
        {
            var student = _mapper.Map<Student>(this);
            _service.UpdateStudent(student);
        }
    }
}

using Autofac;
using AutoMapper;
using MyApp.Training.BusinessObjects;
using MyApp.Training.Services;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Web.Areas.Admin.Models
{
    public class CourseEditModel
    {
        private ICourseService _courseService;
        private ILifetimeScope _scope;
        public IMapper _mapper;

        public int Id { get; set; }

        [Required, MaxLength(255, ErrorMessage = "The length of must be 255")]
        public string? Title { get; set; }

        [Range(0, 50000, ErrorMessage = "Fee should be between 0 - 50000")]
        public double Fees { get; set; }

        public CourseEditModel()
        {

        }

        public CourseEditModel(IMapper mapper,ICourseService courseService)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _courseService = _scope.Resolve<ICourseService>();
            _mapper = _scope.Resolve<IMapper>();
        }

        internal void Update()
        {
            var course = _mapper.Map<Course>(this);

            _courseService.UpdateCourse(course);
        }

        internal void LoadData(int id)
        {
            var course = _courseService.GetCourse(id);
            _mapper.Map(course, this);
        }
    }
}

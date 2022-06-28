using Autofac;
using AutoMapper;
using MyApp.Training.BusinessObjects;
using MyApp.Training.Services;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Web.Areas.Admin.Models
{
    public class CourseCreateModel
    {
        private ICourseService _courseService;
        private ILifetimeScope _scope;
        private IMapper _mapper;

        [Required, MaxLength(255, ErrorMessage ="The length of must be 255")]
        public string Title { get; set; }

        [Range(0,50000, ErrorMessage ="Fee should be between 0 - 50000")]
        public double Fees { get; set; }

        public CourseCreateModel()
        {

        }

        public CourseCreateModel(IMapper mapper,ICourseService courseService)
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

        internal void Create()
        {
            //Map the form data to Business object
            var course = _mapper.Map<Course>(this);

            _courseService.CreateCourse(course);
        }
    }
}

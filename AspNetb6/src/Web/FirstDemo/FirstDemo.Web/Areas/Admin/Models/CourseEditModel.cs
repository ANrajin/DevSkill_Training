using Autofac;
using AutoMapper;
using FirstDemo.Training.BusinessObjects;
using FirstDemo.Training.Services;
using System.ComponentModel.DataAnnotations;

namespace FirstDemo.Web.Areas.Admin.Models
{
    public class CourseEditModel
    {
        private ICourseService _courseService;
        private ILifetimeScope _scope;
        private IMapper _mapper;

        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "Title should be less than 100 chars")]
        public string Title { get; set; }
        [Range(0, 50000, ErrorMessage = "Fees should be between 0 to 50,000")]
        public double Fees { get; set; }

        public CourseEditModel()
        {

        }

        public CourseEditModel(IMapper mapper, ICourseService courseService)
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

        public void EditCourse()
        {
            var course = _mapper.Map<Course>(this);

            _courseService.EditCourse(course);
        }

        internal void LoadData(int id)
        {
            var course = _courseService.GetCourse(id);
            _mapper.Map(course, this);
        }
    }
}

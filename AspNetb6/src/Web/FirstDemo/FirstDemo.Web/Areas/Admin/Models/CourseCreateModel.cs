using Autofac;
using AutoMapper;
using FirstDemo.Training.BusinessObjects;
using FirstDemo.Training.Services;
using System.ComponentModel.DataAnnotations;

namespace FirstDemo.Web.Areas.Admin.Models
{
    public class CourseCreateModel
    {
        private ICourseService _courseService;
        private ILifetimeScope _scope;
        private IMapper _mapper;

        [StringLength(100, ErrorMessage = "Title should be less than 100 chars")]
        public string Title { get; set; }
        [Range(0, 50000, ErrorMessage = "Fees should be between 0 to 50,000")]
        public double Fees { get; set; }
        public List<string> TopicTitle { get; set; }
        public List<string> TopicDescription { get; set; }

        public CourseCreateModel()
        {

        }

        public CourseCreateModel(IMapper mapper, ICourseService courseService)
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

        internal void CreateCourse()
        {
            var course = _mapper.Map<Course>(this);
            course.Topics = new List<Topic>();
            for(int i = 0; i< TopicTitle.Count; i++)
            {
                course.Topics.Add(new Topic
                {
                    Title = TopicTitle[i],
                    Description = TopicDescription[i]
                });
            }
            _courseService.CreateCourse(course);
        }
    }
}

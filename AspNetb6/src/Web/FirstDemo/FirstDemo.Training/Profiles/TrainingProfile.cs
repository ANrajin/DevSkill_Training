using AutoMapper;
using FirstDemo.Training.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseEO = FirstDemo.Training.Entities.Course;
using StudentEO = FirstDemo.Training.Entities.Student;
using TopicEO = FirstDemo.Training.Entities.Topic;

namespace FirstDemo.Training.Profiles
{
    public class TrainingProfile : Profile
    {
        public TrainingProfile()
        {
            CreateMap<CourseEO, Course>()
                .ForMember(dest => dest.Fees, src => src.MapFrom(x => x.Fee))
                .ReverseMap();

            CreateMap<StudentEO, Student>()
                .ReverseMap();

            CreateMap<TopicEO, Topic>()
                .ReverseMap();
        }
    }
}

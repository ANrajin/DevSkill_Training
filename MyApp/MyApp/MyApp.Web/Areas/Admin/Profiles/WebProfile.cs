using AutoMapper;
using MyApp.Training.BusinessObjects;
using MyApp.Web.Areas.Admin.Models;

namespace MyApp.Web.Areas.Admin.Profiles
{
    public class WebProfile:Profile
    {
        public WebProfile()
        {
            CreateMap<CourseCreateModel, Course>()
                .ForMember(dst => dst.Name, src => src.MapFrom(s => s.Title))
                .ReverseMap();
            CreateMap<CourseEditModel, Course>()
                .ForMember(dst => dst.Name, src => src.MapFrom(s => s.Title))
                .ReverseMap();
            CreateMap<StudentCreateModel, Student>().ReverseMap();
            CreateMap<StudentEditModel, Student>().ReverseMap();
        }
    }
}

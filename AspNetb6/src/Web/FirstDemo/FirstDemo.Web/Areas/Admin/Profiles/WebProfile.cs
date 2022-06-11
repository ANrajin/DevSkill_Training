using AutoMapper;
using FirstDemo.Training.BusinessObjects;
using FirstDemo.Web.Areas.Admin.Models;

namespace FirstDemo.Web.Admin.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<CourseCreateModel, Course>()
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Title))
                .ReverseMap();

            CreateMap<CourseEditModel, Course>()
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Title))
                .ReverseMap();

            CreateMap<StudentCreateModel, Student>()
                .ReverseMap();

            CreateMap<StudentEditModel, Student>()
                .ReverseMap();
        }
    }
}

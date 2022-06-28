using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Training.BusinessObjects;
using CourseEO = MyApp.Training.Entities.Course;
using StudentEO = MyApp.Training.Entities.Student;

namespace MyApp.Training.Profiles
{
    public class TrainingProfile:Profile
    {
        public TrainingProfile()
        {
            CreateMap<CourseEO, Course>().ReverseMap();
            CreateMap<StudentEO, Student>().ReverseMap();
        }
    }
}

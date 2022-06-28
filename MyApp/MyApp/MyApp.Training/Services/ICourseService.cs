using MyApp.Training.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Training.Services
{
    public interface ICourseService
    {
        void CreateCourse(Course course);
        (int total, int totalDisplay, IList<Course> records) GetCourses(int pageIndex, int pageSize,
            string searchText, string orderBy);
        Course GetCourse(int id);
        void UpdateCourse(Course course);
        void DeleteCourse(int id);
        IList<Course> GetAllCourses();
    }
}

using FirstDemo.Training.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Training.Services
{
    public interface ICourseService
    {
        void CreateCourse(Course course);
        (int total, int totalDisplay, IList<Course> records) GetCourses(int pageIndex, int pageSize, 
            string searchText, string orderBy);
        void EditCourse(Course course);
        Course GetCourse(int id);
        void DeleteCourse(int id);
        IList<Course> GetCourses();
    }
}

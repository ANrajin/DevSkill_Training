using MyApp.Training.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Training.Services
{
    public interface IStudentService
    {
        Student GetStudent(int id);
        (int total, int totalDisplay, IList<Student> records) GetStudents
            (int pageIndex, int pageSize, string searchText, string orderBy);
        void CreateStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int id);
        IList<Student> GetAllStudents();
    }
}

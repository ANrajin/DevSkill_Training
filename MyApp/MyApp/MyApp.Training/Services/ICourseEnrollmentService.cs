using MyApp.Training.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Training.Services
{
    public interface ICourseEnrollmentService
    {
        void EnrollStudent(Course course, Student student);
    }
}

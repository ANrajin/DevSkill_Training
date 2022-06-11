using FirstDemo.Training.BusinessObjects;

namespace FirstDemo.Training.Services
{
    public interface ICourseEnrollmentService
    {
        void EnrollStudent(Course course, Student student);
    }
}
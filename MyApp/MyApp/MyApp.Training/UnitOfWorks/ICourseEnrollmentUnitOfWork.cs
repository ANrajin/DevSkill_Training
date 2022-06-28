using MyApp.Data;
using MyApp.Training.Repositories;

namespace MyApp.Training.UnitOfWorks
{
    public interface ICourseEnrollmentUnitOfWork:IUnitOfWork
    {
        ICourseRepository Courses { get; }
        IStudentRepository Students { get; }
        IEmailRepository EmailMessages { get; }
    }
}
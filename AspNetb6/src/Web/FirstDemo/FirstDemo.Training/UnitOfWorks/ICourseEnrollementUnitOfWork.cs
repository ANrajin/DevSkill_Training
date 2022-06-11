using FirstDemo.Data;
using FirstDemo.Training.Repositories;

namespace FirstDemo.Training.UnitOfWorks
{
    public interface ICourseEnrollementUnitOfWork : IUnitOfWork
    {
        ICourseRepository Courses { get; }
        IStudentRepository Students { get; }
        IEmailMessageRepository EmailMessages { get; }
    }
}
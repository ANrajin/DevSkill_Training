using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Training.DbContexts;
using MyApp.Training.Repositories;

namespace MyApp.Training.UnitOfWorks
{
    public class CourseEnrollmentUnitOfWork : UnitOfWork, ICourseEnrollmentUnitOfWork
    {
        public ICourseRepository Courses { get; private set; }
        public IStudentRepository Students { get; private set; }
        public IEmailRepository EmailMessages { get; private set; }

        public CourseEnrollmentUnitOfWork(ITrainingDbContext dbContext, 
            ICourseRepository courseRepository,
            IStudentRepository studentRepository,
            IEmailRepository emailRepository) : base((DbContext) dbContext)
        {
            Courses = courseRepository;
            Students = studentRepository;
            EmailMessages = emailRepository;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MyApp.Training.Entities;

namespace MyApp.Training.DbContexts
{
    public interface ITrainingDbContext
    {
        DbSet<Course> Courses { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<EmailMessage> EmailMessages { get; set; }
    }
}
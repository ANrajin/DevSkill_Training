using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Training.DbContexts;
using MyApp.Training.Entities;

namespace MyApp.Training.Repositories
{
    public class CourseRepository : Repository<Course, int>, ICourseRepository
    {
        public CourseRepository(ITrainingDbContext context) : base((DbContext)context)
        {
        }
    }
}

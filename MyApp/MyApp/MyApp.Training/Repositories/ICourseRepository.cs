using MyApp.Data;
using MyApp.Training.Entities;

namespace MyApp.Training.Repositories
{
    public interface ICourseRepository : IRepository<Course, int>
    {
    }
}
using FirstDemo.Data;
using FirstDemo.Training.Entities;

namespace FirstDemo.Training.Repositories
{
    public interface IStudentRepository : IRepository<Student, int>
    {
        Task<(IEnumerable<Student> data, int total, int totalFiltered)> GetStudentsAsync(
            int pageIndex, 
            int pageSize, 
            string orderBy, 
            string searchName, 
            string searchAddress);
    }
}
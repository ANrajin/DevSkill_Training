using FirstDemo.Data;
using FirstDemo.Training.DbContexts;
using FirstDemo.Training.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Training.Repositories
{
    public class StudentRepository : Repository<Student, int>, IStudentRepository
    {
        public StudentRepository(ITrainingDbContext context) : base((DbContext)context)
        {
        }

        public async Task<(IEnumerable<Student> data, int total, int totalFiltered)> GetStudentsAsync(
            int pageIndex, 
            int pageSize, 
            string orderBy, 
            string searchName, 
            string searchAddress)
        {
            var result = await QueryWithStoredProcedureAsync<Student>("GetStudents", new Dictionary<string, object>
                {
                    {"PageIndex", pageIndex},
                    {"PageSize", pageSize},
                    {"OrderBy", orderBy },
                    {"Name", searchName},
                    {"Address", searchAddress}
                },
                new Dictionary<string, Type>
                {
                    {"Total", typeof(int)},
                    {"TotalDisplay", typeof(int)}
                });

            return (result.result, int.Parse(result.outValues.ElementAt(0).Value.ToString()!), 
                int.Parse(result.outValues.ElementAt(1).Value.ToString()!));
        }
    }
}

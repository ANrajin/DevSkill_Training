using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Training.DbContexts;
using MyApp.Training.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Training.Repositories
{
    public class StudentRepository:Repository<Student, int>, IStudentRepository
    {
        public StudentRepository(ITrainingDbContext context):base((DbContext) context)
        {

        }
    }
}

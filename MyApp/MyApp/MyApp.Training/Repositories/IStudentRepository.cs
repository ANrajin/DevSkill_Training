using MyApp.Data;
using MyApp.Training.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Training.Repositories
{
    public interface IStudentRepository:IRepository<Student, int>
    {
    }
}

using FirstDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Training.Entities
{
    public class Student : IEntity<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double CGPA { get; set; }
        public string? Address { get; set; }
        public List<CourseStudent> Courses { get; set; }
    }
}

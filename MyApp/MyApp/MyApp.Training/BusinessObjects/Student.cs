using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Training.BusinessObjects
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? CGPA { get; set; }
        public string? Address { get; set; }
    }
}

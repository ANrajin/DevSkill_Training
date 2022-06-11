using FirstDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Training.Entities
{
    public class Course : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Fee { get; set; }
        public List<Topic> Topics { get; set; }
        public List<CourseStudent> Students { get; set; }
    }
}

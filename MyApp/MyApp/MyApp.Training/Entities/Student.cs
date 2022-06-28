using MyApp.Data;

namespace MyApp.Training.Entities
{
    public class Student:IEntity<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double CGPA { get; set; }
        public string? Address { get; set; }
        public IList<CourseStudent>? Courses { get; set; }
    }
}

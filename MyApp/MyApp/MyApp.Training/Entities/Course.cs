using MyApp.Data;

namespace MyApp.Training.Entities
{
    public class Course:IEntity<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Fees { get; set; }
        public List<Topic>? Topics { get; set; }
        public List<CourseStudent>? Students { get; set; }
    }
}

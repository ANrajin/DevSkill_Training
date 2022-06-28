using MyApp.Data;

namespace MyApp.Training.Entities
{
    public class EmailMessage:IEntity<int>
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }
        public DateTime Created { get; set; }
    }
}

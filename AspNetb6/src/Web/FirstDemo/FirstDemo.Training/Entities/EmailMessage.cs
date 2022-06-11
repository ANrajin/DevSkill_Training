using FirstDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Training.Entities
{
    public class EmailMessage : IEntity<int>
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }
        public DateTime SendTime { get; set; }
    }
}

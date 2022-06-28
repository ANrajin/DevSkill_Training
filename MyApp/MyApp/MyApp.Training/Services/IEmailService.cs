using MyApp.Training.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Training.Services
{
    public interface IEmailService
    {
        IList<EmailMessage> GetAll();
        void Create(string message, string subject, string receiverName, string receiverEmail);
    }
}

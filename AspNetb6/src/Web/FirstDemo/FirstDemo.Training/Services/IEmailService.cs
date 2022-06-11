using FirstDemo.Training.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Training.Services
{
    public interface IEmailService
    {
        void CreateEmail(string subject, string body, string receiverEmail, string receiverName);
        IList<EmailMessage> GetEmails();
    }
}

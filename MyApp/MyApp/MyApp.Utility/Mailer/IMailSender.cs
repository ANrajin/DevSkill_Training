using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Utility.Mailer
{
    public interface IMailSender
    {
        void Send(string recipientName, string recipientEmail, string subject, string message);
    }
}

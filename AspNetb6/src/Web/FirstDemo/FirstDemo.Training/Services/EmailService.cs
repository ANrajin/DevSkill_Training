using AutoMapper;
using FirstDemo.Training.Entities;
using FirstDemo.Training.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Training.Services
{
    public class EmailService : IEmailService
    {
        private readonly ICourseEnrollementUnitOfWork _courseEnrollementUnitOfWork;

        public EmailService(
            ICourseEnrollementUnitOfWork courseEnrollementUnitOfWork)
        {
            _courseEnrollementUnitOfWork = courseEnrollementUnitOfWork;
        }

        public void CreateEmail(string subject, string body, 
            string receiverEmail, string receiverName)
        {
            var email = new EmailMessage
            {
                Subject = subject,
                Message = body,
                ReceiverEmail = receiverEmail,
                ReceiverName = receiverName,
                SendTime = DateTime.Now
            };

            _courseEnrollementUnitOfWork.EmailMessages.Add(email);
            _courseEnrollementUnitOfWork.Save();
        }

        public IList<EmailMessage> GetEmails()
        {
            return _courseEnrollementUnitOfWork.EmailMessages.GetAll();
        }
    }
}

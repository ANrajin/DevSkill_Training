using MyApp.Training.Entities;
using MyApp.Training.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Training.Services
{
    public class EmailService : IEmailService
    {
        private readonly ICourseEnrollmentUnitOfWork _courseEnrollmentUnitOfWork;

        public EmailService(ICourseEnrollmentUnitOfWork courseEnrollmentUnitOfWork)
        {
            _courseEnrollmentUnitOfWork = courseEnrollmentUnitOfWork;
        }

        public void Create(string message, string subject, string receiverName, string receiverEmail)
        {
            var email = new EmailMessage()
            {
                Subject = subject,
                Message = message,
                ReceiverEmail = receiverEmail,
                ReceiverName = receiverName,
                Created = DateTime.Now,
            };

            _courseEnrollmentUnitOfWork.EmailMessages.Add(email);
            _courseEnrollmentUnitOfWork.Save();
        }

        public IList<EmailMessage> GetAll()
        {
            return _courseEnrollmentUnitOfWork.EmailMessages.GetAll();
        }
    }
}

using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Utility.Mailer
{
    public class MailSender : IMailSender
    {
        private SmtpConfiguration? _smtpConfiguration;

        public MailSender(SmtpConfiguration smtpConfiguration)
        {
            _smtpConfiguration = smtpConfiguration;
        }

        public void Send(string recipientName, string recipientEmail, string subject, string message)
        {
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress(_smtpConfiguration?.SenderName,
                _smtpConfiguration?.SenderEmail));
            msg.From.Add(new MailboxAddress(recipientName, recipientEmail));
            msg.Subject = subject;

            msg.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using(var client = new SmtpClient())
            {
                client.Connect(_smtpConfiguration?.Server, 
                    _smtpConfiguration.Port,
                    _smtpConfiguration.UseSSL);
                client.Authenticate(_smtpConfiguration.Username,
                    _smtpConfiguration.Password);
                client.Send(msg);
                client.Disconnect(true);
            }
        }
    }
}

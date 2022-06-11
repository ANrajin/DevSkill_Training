using MailKit.Net.Smtp;
using MimeKit;

namespace FirstDemo.Utility
{
    public class EmailSender : IEmailSender
    {
        private SmtpConfiguration _smtpConfiguration;

        public EmailSender(SmtpConfiguration smtpConfiguration)
        {
            _smtpConfiguration = smtpConfiguration;
        }

        public void Send(string subject, string body, string receiverEmail, string receiverName)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpConfiguration.SenderName, 
                _smtpConfiguration.SenderEmail));
            message.To.Add(new MailboxAddress(receiverName, receiverEmail));
            message.Subject = subject;

            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Connect(_smtpConfiguration.Server, _smtpConfiguration.Port,
                    _smtpConfiguration.UseSSL);
                client.Authenticate(_smtpConfiguration.Username, _smtpConfiguration.Password);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
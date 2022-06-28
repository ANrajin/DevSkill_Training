using MyApp.Training.Services;
using MyApp.Utility.Mailer;

namespace MyApp.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMailSender _mailSender;
        private readonly IEmailService _emailService;

        public Worker(ILogger<Worker> logger, 
            IMailSender mailSender,
            IEmailService emailService)
        {
            _logger = logger;
            _mailSender = mailSender;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var emails = _emailService.GetAll();

            if(emails.Count > 0)
            {
                foreach(var email in emails)
                {
                    try
                    {
                        _mailSender.Send(email.ReceiverName,
                            email.ReceiverEmail,
                            email.Subject,
                            email.Message);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            await Task.Delay(60000, stoppingToken);
        }
    }
}
using FirstDemo.Training.Services;
using FirstDemo.Utility;

namespace FirstDemo.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IEmailService _emailService;

        public Worker(ILogger<Worker> logger, IEmailSender emailSender, 
            IEmailService emailService)
        {
            _logger = logger;
            _emailSender = emailSender;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var emails = _emailService.GetEmails();
                if(emails != null)
                {
                    foreach(var email in emails)
                    {
                        try
                        {
                            _emailSender.Send(email.Subject, email.Message,
                                email.ReceiverEmail, email.ReceiverName);
                        }
                        catch(Exception ex)
                        {

                        }
                    }
                }
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
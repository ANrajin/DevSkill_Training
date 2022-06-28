using Autofac;
using MyApp.Utility.Mailer;

namespace MyApp.Worker
{
    public class WorkerModule:Module
    {
        private readonly IConfiguration _configuration;
        private readonly SmtpConfiguration _smtpConfiguration;

        public WorkerModule(IConfiguration configuration)
        {
            _configuration = configuration;
            var smtpCred = _configuration.GetSection("SmtpConfiguration"); 
            _smtpConfiguration = new SmtpConfiguration()
            {
                Server = smtpCred["Server"],
                Port = int.Parse(smtpCred["Port"]),
                Username = smtpCred["Username"],
                Password = smtpCred["Password"],
                UseSSL = bool.Parse(smtpCred["UseSSL"]),
                SenderName = smtpCred["SenderName"],
                SenderEmail = smtpCred["SenderEmail"]
            };
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MailSender>().As<IMailSender>()
                .WithParameter("smtpConfiguration", _smtpConfiguration)
                .InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}

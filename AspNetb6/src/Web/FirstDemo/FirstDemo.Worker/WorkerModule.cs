using Autofac;
using FirstDemo.Utility;

namespace FirstDemo.Web
{
    public class WorkerModule : Module
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _migrationAssembly;
        private readonly SmtpConfiguration _smtpConfiguration;

        public WorkerModule(string connectionString, string migrationAssembly,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;

            var smtpSection = _configuration.GetSection("SmtpConfiguration");

            _smtpConfiguration = new SmtpConfiguration()
            {
                Server = smtpSection["Server"],
                Port = int.Parse(smtpSection["Port"]),
                Username = smtpSection["Username"],
                Password = smtpSection["Password"],
                UseSSL = bool.Parse(smtpSection["UseSSL"]),
                SenderName = smtpSection["SenderName"],
                SenderEmail = smtpSection["SenderEmail"]
            };
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailSender>().As<IEmailSender>()
                .WithParameter("smtpConfiguration", _smtpConfiguration)
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}

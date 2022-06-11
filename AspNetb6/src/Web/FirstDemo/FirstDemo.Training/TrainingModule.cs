using Autofac;
using FirstDemo.Training.DbContexts;
using FirstDemo.Training.Repositories;
using FirstDemo.Training.Services;
using FirstDemo.Training.UnitOfWorks;

namespace FirstDemo.Training
{
    public class TrainingModule : Module
    {
        private readonly string _connectionString;
        private readonly string _assemblyName;

        public TrainingModule(string connectionString, string assemblyName)
        {
            _connectionString = connectionString;
            _assemblyName = assemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TrainingDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("assemblyName", _assemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<TrainingDbContext>().As<ITrainingDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("assemblyName", _assemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<CourseEnrollementUnitOfWork>().As<ICourseEnrollementUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CourseRepository>().As<ICourseRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CourseService>().As<ICourseService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StudentRepository>().As<IStudentRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmailMessageRepository>().As<IEmailMessageRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StudentService>().As<IStudentService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmailService>().As<IEmailService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CourseEnrollmentService>().As<ICourseEnrollmentService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }


}

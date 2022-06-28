using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Training.DbContexts;
using MyApp.Training.Repositories;
using MyApp.Training.UnitOfWorks;
using MyApp.Training.Services;

namespace MyApp.Training
{
    public class TrainingModule:Module
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

            builder.RegisterType<CourseEnrollmentUnitOfWork>().As<ICourseEnrollmentUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CourseRepository>().As<ICourseRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<CourseService>().As<ICourseService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StudentRepository>().As<IStudentRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StudentService>().As<IStudentService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CourseEnrollmentService>().As<ICourseEnrollmentService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmailRepository>().As<IEmailRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmailService>().As<IEmailService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}

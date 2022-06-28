using Autofac;
using MyApp.Web.Areas.Admin.Models;
using MyApp.Web.Models;

namespace MyApp.Web
{
    public class WebModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<Books>().As<IBooks>()
            //.InstancePerLifetimeScope();
            builder.RegisterType<CourseViewModel>().AsSelf();
            builder.RegisterType<CourseListModel>().AsSelf();
            builder.RegisterType<CourseCreateModel>().AsSelf();
            builder.RegisterType<CourseEditModel>().AsSelf();

            builder.RegisterType<StudentListModel>().AsSelf();
            builder.RegisterType<StudentCreateModel>().AsSelf();
            builder.RegisterType<StudentEditModel>().AsSelf();

            builder.RegisterType<EnrollmentCreateModel>().AsSelf();

            builder.RegisterType<LoginModel>().AsSelf();
            builder.RegisterType<RegisterModel>().AsSelf();
            builder.RegisterType<RegisterConfirmationModel>().AsSelf();

            base.Load(builder);
        }
    }
}

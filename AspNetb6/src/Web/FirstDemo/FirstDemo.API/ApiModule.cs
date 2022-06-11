using Autofac;
using FirstDemo.API.Models;

namespace FirstDemo.Api
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CourseListModel>().AsSelf();
            
            base.Load(builder);
        }
    }


}

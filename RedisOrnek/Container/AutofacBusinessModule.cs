using Autofac;
using Autofac.Extras.DynamicProxy;
using RedisOrnek.Services;

namespace RedisOrnek.Container
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Data Access ve Business class larının resolve edilmesi bu şekildedir. Örnek olması amacıyla yazılmıştır.
            // builder.RegisterType<StudentManager>().As<IStudentManager>();
            // builder.RegisterType<EfStudentsDal>().As<IStudensDal>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder
                .RegisterAssemblyTypes(assembly)
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                .SingleInstance();

            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
        }
    }
}

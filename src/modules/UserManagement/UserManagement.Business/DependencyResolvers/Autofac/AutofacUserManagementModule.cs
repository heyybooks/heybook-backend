using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using UserManagement.Business.Abstract;
using UserManagement.Business.Concrete;
using UserManagement.Business.Mapping;
using UserManagement.DataAccess.Abstract;
using UserManagement.DataAccess.EntityFramework;

namespace UserManagement.Business.DependencyResolvers.Autofac
{
    public class AutofacUserManagementModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // AutoMapper Konfigürasyonu
            builder.Register(context => new MapperConfiguration(cfg => 
            {
                cfg.AddProfile<UserProfile>();
            })).AsSelf().SingleInstance();

            builder.Register(c => 
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper();
            }).As<IMapper>().InstancePerLifetimeScope();

            // Bağımlılıkları Kaydet
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            // Assembly üzerinden otomatik kayıt
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                });
        }
    }
}

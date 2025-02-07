using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
//using System.Reflection;
using UserManagement.Business.Abstract;
using UserManagement.Business.Concrete;
using UserManagement.DataAccess.Abstract;
using UserManagement.DataAccess.EntityFramework;

namespace UserManagement.Business.DependencyResolvers.Autofac
{
    public class AutofacUserManagementModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // **BU KISIM GEREKSİZ OLDUĞU İÇİN ÇIKARILDI** ❌
            // builder.RegisterModule(new AutoMapperModule(typeof(UserProfile).Assembly));

            // **Manager Bağımlılıkları**
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();

            // **Dal Bağımlılıkları**
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();

            // **AOP Entegrasyonu**
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}

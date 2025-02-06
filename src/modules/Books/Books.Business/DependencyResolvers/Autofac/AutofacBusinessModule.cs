using Autofac;
using Autofac.Extras.DynamicProxy;
using Books.Business.Abstract;
using Books.Business.Concrete;
using Books.Business.Mapping;
using Books.DataAccess.Abstract;
using Books.DataAccess.EntityFramework;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
//using System.Reflection;

namespace Books.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // **BU KISIM GEREKSİZ OLDUĞU İÇİN ÇIKARILDI** ❌
            // builder.RegisterModule(new AutoMapperModule(typeof(BookProfile).Assembly));

            // **Manager Bağımlılıkları**
            builder.RegisterType<BookManager>().As<IBookService>().SingleInstance();

            // **Dal Bağımlılıkları**
            builder.RegisterType<EfBookDal>().As<IBookDal>().SingleInstance();
            builder.RegisterType<EfBookImageDal>().As<IBookImageDal>().SingleInstance();

            builder.RegisterType<BookFactory>().AsSelf().SingleInstance();

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

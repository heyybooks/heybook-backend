using Autofac;
using Autofac.Extras.DynamicProxy;
using Books.Business.Abstract;
using Books.Business.Concrete;
using Books.DataAccess.Abstract;
using Books.DataAccess.EntityFramework;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;

namespace Books.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookManager>().As<IBookService>().SingleInstance();
            builder.RegisterType<EfBookDal>().As<IBookDal>().SingleInstance();
            builder.RegisterType<EfBookImageDal>().As<IBookImageDal>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
             .EnableInterfaceInterceptors(new ProxyGenerationOptions()
             {
                 Selector = new AspectInterceptorSelector()
             }).SingleInstance();
        }
    }
}

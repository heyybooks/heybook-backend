using Autofac;
using AutoMapper;
using System.Linq;
//using System.Reflection;

namespace Core.Mapping
{
    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx =>
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                var config = new MapperConfiguration(cfg =>
                {
                    // **TÜM PROFİLLERİ OTOMATİK OLARAK YÜKLE** ✅
                    cfg.AddMaps(assemblies);
                });

                return config;
            }).AsSelf().SingleInstance();

            // **AutoMapper Instance'ını Her Modülde Kullanılabilir Hale Getir**
            builder.Register(ctx =>
            {
                var config = ctx.Resolve<MapperConfiguration>();
                return config.CreateMapper();
            }).As<IMapper>().SingleInstance(); // **Tek bir örnek oluştur!**
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Swap.Business.Abstract;
using Swap.Business.Concrete;
using Swap.DataAccess.Abstract;
using Swap.DataAccess.EntityFramework;

namespace Swap.Business.DependencyResolvers
{
    public static class ServiceRegistration
    {
        public static void AddSwapServices(this IServiceCollection services)
        {
            services.AddScoped<ISwapService, SwapManager>();
            services.AddScoped<ISwapDal, EfSwapDal>();
            services.AddScoped<ISwapRatingDal, EfSwapRatingDal>();
        }
    }
}
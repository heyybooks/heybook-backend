using AutoMapper;
using Core.Mapping;

namespace UserManagement.Business.Mapping
{
    public class UserAutoMapperConfiguration : IAutoMapperConfigurator
    {
        public void Configure(IMapperConfigurationExpression config)
        {
            // UserProfile'ı ekle
            config.AddProfile<UserProfile>();
        }
    }
}

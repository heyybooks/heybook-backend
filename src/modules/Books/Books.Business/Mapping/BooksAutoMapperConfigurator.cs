using AutoMapper;
using Core.Mapping;

namespace Books.Business.Mapping
{
    public class BooksAutoMapperConfigurator : IAutoMapperConfigurator
    {
        public void Configure(IMapperConfigurationExpression config)
        {
            // BookProfile'ı ekle
            config.AddProfile<BookProfile>();
        }
    }
}

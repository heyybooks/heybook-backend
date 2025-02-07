using AutoMapper;
using Core.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping
{
    public interface IAutoMapperConfigurator
    {
        public void Configure(IMapperConfigurationExpression config);
    }
}



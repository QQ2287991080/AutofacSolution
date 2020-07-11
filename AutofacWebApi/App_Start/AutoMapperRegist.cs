using AutofacWebApi.Models;
using AutoMapper;
using Domian;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Extensions.EnumMapping;
namespace AutofacWebApi.App_Start
{
    public class AutoMapperRegist
    {
        public IMapper Register()
        {
            var configuration = new MapperConfiguration(c =>
            {
                c.AddProfile(new EnTypeMapper());
            });
            configuration.AssertConfigurationIsValid();
            return new Mapper(configuration);
        }
    }

    public class EnTypeMapper : Profile, IProfile
    {
        public EnTypeMapper()
        {
            CreateMap<BA_SysEnType, EntypeModel>();

            CreateMap<EntypeModel, BA_SysEnType>();
        }
    }

    public interface IProfile
    { 
    
    }
}

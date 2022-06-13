using AutoMapper;
using Service.Security.Api.Core.Entities;

namespace Service.Security.Api.Core.Dto
{
    public class Mapping: Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDto>();
        }
    }
}

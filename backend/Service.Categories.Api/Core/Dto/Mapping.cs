using AutoMapper;
using Service.Categories.Api.Core.Entities;

namespace Service.Categories.Api.Core.Dto
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}

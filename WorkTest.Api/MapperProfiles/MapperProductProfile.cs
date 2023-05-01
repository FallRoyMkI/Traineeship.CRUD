using WorkTest.Api.Models.Product.Response;
using WorkTest.Bll.Models;
using WorkTest.Dal.Models;
using AutoMapper;

namespace WorkTest.Api.MapperProfiles;

public class MapperProductProfile : Profile
{
    public MapperProductProfile()
    {
        CreateMap<Product, ProductResponse>().ReverseMap();
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}
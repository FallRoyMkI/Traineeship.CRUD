using AutoMapper;
using WorkTest.Bll.Models;
using WorkTest.Models.Dto.OrderLine;
using WorkTest.Models.Entity;

namespace WorkTest.Api.MapperProfiles;

public class MapperProductProfile : Profile
{
    public MapperProductProfile()
    {
        CreateMap<OrderLineModel, OrderLineResponseDto>().ReverseMap();
        CreateMap<OrderLineModel, OrderLineEntity>().ReverseMap();
        CreateMap<OrderLineRequestDto, OrderLineModel>();
    }
}
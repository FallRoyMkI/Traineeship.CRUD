using AutoMapper;
using WorkTest.Bll.Models;
using WorkTest.Models.Dto.Order;
using WorkTest.Models.Dto.OrderLine;
using WorkTest.Models.Entity;
using WorkTest.Models.Model;

namespace WorkTest.Api.MapperProfiles;

public class MapperOrderProfile : Profile
{
    public MapperOrderProfile()
    {
        CreateMap<OrderAddRequestDto, OrderModel>();
        CreateMap<OrderUpdateRequestDto, OrderModel>();
        CreateMap<OrderModel, OrderEntity>().ReverseMap();
        CreateMap<OrderModel, OrderResponseDto>().ForMember(x => x.Created,
            p => p.MapFrom(y => y.Created.ToString("G")));
        
    }
}
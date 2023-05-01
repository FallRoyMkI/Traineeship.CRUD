using WorkTest.Api.Models.Order.Response;
using WorkTest.Api.Models.Order.Request;
using WorkTest.Bll.Models;
using WorkTest.Dal.Models;
using AutoMapper;

namespace WorkTest.Api.MapperProfiles;

public class MapperOrderProfile : Profile
{
    public MapperOrderProfile()
    {
        CreateMap<OrderAddRequest, Order>();
        CreateMap<OrderUpdateRequest, Order>();
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<Order, OrderResponse>();
    }
}
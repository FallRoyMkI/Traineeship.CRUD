using AutoMapper;
using WorkTest.Bll.Interfaces;
using WorkTest.Bll.Models;
using WorkTest.Constants;
using WorkTest.Constants.Exceptions;
using WorkTest.Dal.Interfaces;
using WorkTest.Dal.Models;

namespace WorkTest.Bll;

public sealed class OrderManager : IOrderManager
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;

    public OrderManager(IMapper mapper, IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public Order CreateOrder(Order order)
    {
        IsGuidExist(order.Id);

        order.Status = OrderStatus.New;
        order.Created = DateTime.Now.ToUniversalTime();

        OrderDto dto = _mapper.Map<OrderDto>(order);
        OrderDto callback = _orderRepository.CreateNewOrder(dto);

        return _mapper.Map<Order>(callback);
    }

    public Order UpdateOrder(Guid guid, Order order)
    {
        IsOrderExist(guid);

        OrderDto dto = _orderRepository.GetOrderById(guid);
        IsOrderDeleted(ref dto);
        IsAllowedToUpdate(ref dto);

        dto.Status = order.Status;
        dto.Lines = _mapper.Map<OrderDto>(order).Lines;

        OrderDto callback = _orderRepository.UpdateOrder(dto);

        return _mapper.Map<Order>(callback);
    }

    public void DeleteOrder(Guid guid)
    {
        IsOrderExist(guid);

        OrderDto dto = _orderRepository.GetOrderById(guid);
        IsOrderDeleted(ref dto);
        IsAllowedToDelete(ref dto);

        _orderRepository.DeleteOrder(dto);
    }

    public Order GetOrderById(Guid guid)
    {
        IsOrderExist(guid);

        OrderDto dto = _orderRepository.GetOrderById(guid);
        IsOrderDeleted(ref dto);

        return _mapper.Map<Order>(dto);
    }

    private void IsOrderExist(Guid guid)
    {
        if (!_orderRepository.IsOrderExist(guid))
        {
            throw new EntityNotFoundException("Заказ с таким Id не найден");
        }
    }

    private void IsOrderDeleted(ref OrderDto dto)
    {
        if (dto.IsDeleted)
        {
            throw new AttemptToGetDeletedOrderException("Заказ удален");
        }
    }

    private void IsAllowedToDelete(ref OrderDto dto)
    {
        if (dto.Status >= (OrderStatus)3)
        {
            throw new NotAllowedToDeleteOrderException
                ("Заказы в статусах “передан в доставку”, “доставлен”, “завершен” нельзя удалить");
        }
    }

    private void IsAllowedToUpdate(ref OrderDto dto)
    {
        if (dto.Status >= (OrderStatus)2)
        {
            throw new NotAllowToEditEntityException
                ("Заказы в статусах “оплачен”, “передан в доставку”, “доставлен”, “завершен” нельзя редактировать");
        }
    }

    private void IsGuidExist(Guid id)
    {
        if (_orderRepository.IsOrderExist(id))
        {
            throw new OrderGuidAlreadyExistException("Заказ с таким Guid уже существует в базе");
        }
    }
}
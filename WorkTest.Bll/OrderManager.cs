using AutoMapper;
using WorkTest.Bll.Exceptions;
using WorkTest.Contracts;
using WorkTest.Models.Entity;
using WorkTest.Models.Enum;
using WorkTest.Models.Model;

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

    // public orderDTO ** (orderDTO)

    public OrderModel CreateOrder(OrderModel order)
    {
        IsGuidExist(order.Id);

        order.Status = OrderStatus.New;
        order.Created = DateTime.Now.ToUniversalTime();

        OrderEntity dto = _mapper.Map<OrderEntity>(order);
        OrderEntity callback = _orderRepository.CreateNewOrder(dto);

        return _mapper.Map<OrderModel>(callback);
    }

    public OrderModel UpdateOrder(Guid guid, OrderModel order)
    {
        IsOrderExist(guid);

        OrderEntity dto = _orderRepository.GetOrderById(guid);
        IsOrderDeleted(ref dto);
        IsAllowedToUpdate(ref dto);

        dto.Status = order.Status;
        dto.Lines = _mapper.Map<OrderEntity>(order).Lines;

        OrderEntity callback = _orderRepository.UpdateOrder(dto);

        return _mapper.Map<OrderModel>(callback);
    }

    public void DeleteOrder(Guid guid)
    {
        IsOrderExist(guid);

        OrderEntity dto = _orderRepository.GetOrderById(guid);
        IsOrderDeleted(ref dto);
        IsAllowedToDelete(ref dto);

        _orderRepository.DeleteOrder(dto);
    }

    public OrderModel GetOrderById(Guid guid)
    {
        IsOrderExist(guid);

        OrderEntity dto = _orderRepository.GetOrderById(guid);
        IsOrderDeleted(ref dto);

        return _mapper.Map<OrderModel>(dto);
    }

    private void IsOrderExist(Guid guid)
    {
        if (!_orderRepository.IsOrderExist(guid))
        {
            throw new EntityNotFoundException("Заказ с таким Id не найден");
        }
    }

    private void IsOrderDeleted(ref OrderEntity dto)
    {
        if (dto.IsDeleted)
        {
            throw new AttemptToGetDeletedOrderException("Заказ удален");
        }
    }

    private void IsAllowedToDelete(ref OrderEntity dto)
    {
        if (dto.Status >= (OrderStatus)3)
        {
            throw new NotAllowedToDeleteOrderException
                ("Заказы в статусах “передан в доставку”, “доставлен”, “завершен” нельзя удалить");
        }
    }

    private void IsAllowedToUpdate(ref OrderEntity dto)
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
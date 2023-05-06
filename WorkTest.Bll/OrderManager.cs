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

    public OrderModel CreateOrder(OrderModel order)
    {
        IsGuidExist(order.Id);

        order.Status = OrderStatus.New;
        order.Created = DateTime.Now.ToUniversalTime();

        OrderEntity entity = _mapper.Map<OrderEntity>(order);
        OrderEntity callback = _orderRepository.CreateNewOrder(entity);

        return _mapper.Map<OrderModel>(callback);
    }

    public OrderModel UpdateOrder(OrderModel order)
    {
        IsOrderExist(order.Id);

        OrderEntity entity = _orderRepository.GetOrderById(order.Id);
        IsOrderDeleted(ref entity);
        IsAllowedToUpdate(ref entity);

        entity.Status = order.Status;
        entity.Lines = _mapper.Map<OrderEntity>(order).Lines;

        OrderEntity callback = _orderRepository.UpdateOrder(entity);

        return _mapper.Map<OrderModel>(callback);
    }

    public void DeleteOrder(Guid guid)
    {
        IsOrderExist(guid);

        OrderEntity entity = _orderRepository.GetOrderById(guid);
        IsOrderDeleted(ref entity);
        IsAllowedToDelete(ref entity);

        _orderRepository.DeleteOrder(entity);
    }

    public OrderModel GetOrderById(Guid guid)
    {
        IsOrderExist(guid);

        OrderEntity entity = _orderRepository.GetOrderById(guid);
        IsOrderDeleted(ref entity);

        return _mapper.Map<OrderModel>(entity);
    }

    private void IsOrderExist(Guid guid)
    {
        if (!_orderRepository.IsOrderExist(guid))
        {
            throw new EntityNotFoundException("Заказ с таким Id не найден");
        }
    }

    private void IsOrderDeleted(ref OrderEntity entity)
    {
        if (entity.IsDeleted)
        {
            throw new AttemptToGetDeletedOrderException("Заказ удален");
        }
    }

    private void IsAllowedToDelete(ref OrderEntity entity)
    {
        if (entity.Status is OrderStatus.TransferredForDelivery or OrderStatus.Delivered or OrderStatus.Completed)
        {
            throw new NotAllowedToDeleteOrderException
                ("Заказы в статусах “передан в доставку”, “доставлен”, “завершен” нельзя удалить");
        }
    }

    private void IsAllowedToUpdate(ref OrderEntity entity)
    {
        if (entity.Status is OrderStatus.Paid or OrderStatus.TransferredForDelivery or OrderStatus.Delivered or OrderStatus.Completed)
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
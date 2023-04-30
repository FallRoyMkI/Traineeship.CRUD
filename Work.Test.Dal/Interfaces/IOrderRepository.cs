using WorkTest.Dal.Models;

namespace WorkTest.Dal.Interfaces;

public interface IOrderRepository
{
    public OrderDto CreateNewOrder(OrderDto order);
    public OrderDto UpdateOrder(OrderDto order);
    public void DeleteOrder(OrderDto order);
    public bool IsOrderExist(Guid guid);
    public OrderDto GetOrderById(Guid guid);
}
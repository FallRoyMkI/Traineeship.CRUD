using WorkTest.Models.Entity;

namespace WorkTest.Contracts;

public interface IOrderRepository
{
    public OrderEntity CreateNewOrder(OrderEntity order);
    public OrderEntity UpdateOrder(OrderEntity order);
    public void DeleteOrder(OrderEntity order);
    public bool IsOrderExist(Guid guid);
    public OrderEntity GetOrderById(Guid guid);
}
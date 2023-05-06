using WorkTest.Models.Model;

namespace WorkTest.Contracts;

public interface IOrderManager
{
    public OrderModel CreateOrder(OrderModel order);
    public OrderModel UpdateOrder(OrderModel order);
    public void DeleteOrder(Guid guid);
    public OrderModel GetOrderById(Guid guid);
}
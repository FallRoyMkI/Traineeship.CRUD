using WorkTest.Bll.Models;

namespace WorkTest.Bll.Interfaces;

public interface IOrderManager
{
    public Order CreateOrder(Order order);
    public Order UpdateOrder(Guid guid, Order order);
    public void DeleteOrder(Guid guid);
    public Order GetOrderById(Guid guid);
}
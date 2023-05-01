using Microsoft.EntityFrameworkCore;
using WorkTest.Contracts;
using WorkTest.Models.Entity;

namespace WorkTest.Dal;

public class OrderRepository : IOrderRepository
{
    private readonly Context _context;

    public OrderRepository(Context context)
    {
        _context = context;
    }

    public OrderEntity CreateNewOrder(OrderEntity order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();

        return order;
    }

    public OrderEntity UpdateOrder(OrderEntity order)
    {
        _context.Orders.Update(order);
        _context.SaveChanges();

        return order;
    }

    public void DeleteOrder(OrderEntity order)
    {
        order.IsDeleted = true;

        _context.Orders.Update(order);
        _context.SaveChanges();
    }

    public OrderEntity GetOrderById(Guid guid)
    {
        return _context.Set<OrderEntity>().Include(order => order.Lines).First(x => x.Id == guid);
    }

    public bool IsOrderExist(Guid guid) => _context.Orders.ToList().Exists(x => x.Id == guid);
}
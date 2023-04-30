using Microsoft.EntityFrameworkCore;
using WorkTest.Dal.Interfaces;
using WorkTest.Dal.Models;

namespace WorkTest.Dal;

public class OrderRepository : IOrderRepository
{
    private readonly Context _context;

    public OrderRepository(Context context)
    {
        _context = context;
    }

    public OrderDto CreateNewOrder(OrderDto order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();

        return order;
    }

    public OrderDto UpdateOrder(OrderDto order)
    {
        _context.Orders.Update(order);
        _context.SaveChanges();

        return order;
    }

    public void DeleteOrder(OrderDto order)
    {
        order.IsDeleted = true;

        _context.Orders.Update(order);
        _context.SaveChanges();
    }

    public OrderDto GetOrderById(Guid guid)
    { 
        return _context.Set<OrderDto>().Include(order => order.Lines).First(x => x.Id == guid);
    }

    public bool IsOrderExist(Guid guid) => _context.Orders.ToList().Exists(x => x.Id == guid);
}
using WorkTest.Constants;

namespace WorkTest.Bll.Models;

public class Order
{
    public Guid Id { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime Created { get; set; }
    public List<Product> Lines { get; set; }
}
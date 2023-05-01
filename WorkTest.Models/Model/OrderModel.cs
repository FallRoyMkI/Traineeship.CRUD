using WorkTest.Bll.Models;
using WorkTest.Models.Enum;

namespace WorkTest.Models.Model;

public class OrderModel
{
    public Guid Id { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime Created { get; set; }
    public List<OrderLineModel> Lines { get; set; }
}
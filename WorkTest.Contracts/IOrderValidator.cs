using WorkTest.Models.Model;

namespace WorkTest.Contracts;

public interface IOrderValidator
{
    public void OrderCreateModelValidator(OrderModel order);
    public void OrderUpdateModelValidator(OrderModel order);
}
using WorkTest.Bll.Models;

namespace WorkTest.Validator;

public interface IOrderValidator
{
    public void OrderCreateModelValidator(Order order);
    public void OrderUpdateModelValidator(Order order);
}
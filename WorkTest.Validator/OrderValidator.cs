using WorkTest.Contracts;
using WorkTest.Models.Enum;
using WorkTest.Models.Model;
using WorkTest.Validator.Exceptions;

namespace WorkTest.Validator;

public class OrderValidator : IOrderValidator
{
    public void OrderCreateModelValidator(OrderModel order)
    {
        IsWithoutLinesValidation(ref order);
        QuantityValidation(ref order);
        UniqueOrderLineIdValidation(ref order);
    }
    public void OrderUpdateModelValidator(OrderModel order)
    {
        UnprocessableStatusValidation(ref order);
        IsWithoutLinesValidation(ref order);
        QuantityValidation(ref order);
        UniqueOrderLineIdValidation(ref order);
    }

    private void IsWithoutLinesValidation(ref OrderModel order)
    {
        if (order.Lines.Count == 0)
        {
            throw new OrderWithoutLinesException("Невозможно создать заказ без строк");
        }
    }
    private void QuantityValidation(ref OrderModel order)
    {
        if (order.Lines.Any(x => x.Qty < 1))
        {
            throw new LineWithNegativeOrZeroQuantityException
                ("Количество по строке заказа должно быть положительным");
        }
    }

    private void UnprocessableStatusValidation(ref OrderModel order)
    {
        if (order.Status is not (OrderStatus.Completed or OrderStatus.Delivered or OrderStatus.Paid 
            or OrderStatus.TransferredForDelivery or OrderStatus.New or OrderStatus.WaitingForPayment))
        {
            throw new StatusNotExistException("Попытка добавить не существующий статус");
        }
    }

    private void UniqueOrderLineIdValidation(ref OrderModel order)
    {
        List<Guid> unique = new();
        foreach (var line in order.Lines.Where(line => !unique.Contains(line.OrderLineId)))
        {
            unique.Add(line.OrderLineId);
        }

        if (order.Lines.Count != unique.Count)
        {
            throw new DifferentLinesWithSameProductException("Попытка добавить одинаковый товар разными строками");
        }
    }
}
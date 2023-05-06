using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkTest.Contracts;
using WorkTest.Models.Dto;
using WorkTest.Models.Dto.Order;
using WorkTest.Models.Model;

namespace WorkTest.Api.Controllers;

public class OrderController : Controller
{
    private readonly IMapper _mapper;
    private readonly IOrderManager _orderManager;
    private readonly IOrderValidator _orderValidator;

    public OrderController(IMapper mapper, IOrderManager orderManager, IOrderValidator orderValidator)
    {
        _mapper = mapper;
        _orderManager = orderManager;
        _orderValidator = orderValidator;
    }

    [HttpPost("/orders")]
    [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponseDto), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ExceptionResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponseDto), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ExceptionResponseDto), 1000)]
    public IActionResult CreateOrder([FromBody] OrderAddRequestDto order)
    {
        OrderModel model = _mapper.Map<OrderModel>(order);
        _orderValidator.OrderCreateModelValidator(model);

        OrderModel callback = _orderManager.CreateOrder(model);
        OrderResponseDto result = _mapper.Map<OrderResponseDto>(callback);

        return Ok(result);
    }

    [HttpPut("/orders/{guid:guid}")]
    [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponseDto), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ExceptionResponseDto), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ExceptionResponseDto), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ExceptionResponseDto), 1000)]
    
    public IActionResult UpdateOrder([FromRoute] Guid guid, [FromBody] OrderUpdateRequestDto order)
    {
        OrderModel model = _mapper.Map<OrderModel>(order);
        _orderValidator.OrderUpdateModelValidator(model);
        model.Id = guid;

        OrderModel callback = _orderManager.UpdateOrder(model);
        OrderResponseDto result = _mapper.Map<OrderResponseDto>(callback);

        return Ok(result);
    }

    [HttpDelete("/orders/{guid:guid}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponseDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ExceptionResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponseDto), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ExceptionResponseDto), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ExceptionResponseDto), 1000)]
    public IActionResult DeleteOrder([FromRoute] Guid guid)
    {
        _orderManager.DeleteOrder(guid);
        return Ok(string.Empty);
    }

    [HttpGet("/orders/{guid:guid}")]
    [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponseDto), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ExceptionResponseDto), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ExceptionResponseDto), 1000)]
    public IActionResult GetOrderById([FromRoute] Guid guid)
    {
        OrderModel callback = _orderManager.GetOrderById(guid);
        OrderResponseDto result = _mapper.Map<OrderResponseDto>(callback);

        return Ok(result);
    }
}
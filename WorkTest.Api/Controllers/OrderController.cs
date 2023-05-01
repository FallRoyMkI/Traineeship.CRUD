using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkTest.Api.Models.Order.Request;
using WorkTest.Api.Models.Order.Response;
using WorkTest.Bll.Interfaces;
using WorkTest.Bll.Models;
using WorkTest.Constants;
using WorkTest.Validator;

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
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult CreateOrder([FromBody] OrderAddRequest order)
    {
        Order model = _mapper.Map<Order>(order);
        _orderValidator.OrderCreateModelValidator(model);

        Order callback = _orderManager.CreateOrder(model);
        OrderResponse result = _mapper.Map<OrderResponse>(callback);

        return Ok(result);
    }

    [HttpPut("/orders/{guid:guid}")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateOrder([FromRoute] Guid guid, [FromBody] OrderUpdateRequest order)
    {
        Order model = _mapper.Map<Order>(order);
        _orderValidator.OrderUpdateModelValidator(model);

        Order callback = _orderManager.UpdateOrder(guid, model);
        OrderResponse result = _mapper.Map<OrderResponse>(callback);

        return Ok(result);
    }

    [HttpDelete("/orders/{guid:guid}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteOrder([FromRoute] Guid guid)
    {
        _orderManager.DeleteOrder(guid);
        return Ok(string.Empty);
    }

    [HttpGet("/orders/{guid:guid}")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult GetOrderById([FromRoute] Guid guid)
    {
        Order callback = _orderManager.GetOrderById(guid);
        OrderResponse result = _mapper.Map<OrderResponse>(callback);

        return Ok(result);
    }
}
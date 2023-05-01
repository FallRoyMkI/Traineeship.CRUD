using System.Text.Json.Serialization;
using WorkTest.Models.Dto.OrderLine;
using WorkTest.Models.Enum;

namespace WorkTest.Models.Dto.Order;

public class OrderUpdateRequestDto
{
    [JsonPropertyName("status")]
    public OrderStatus Status { get; set; }

    [JsonPropertyName("lines")]
    public List<OrderLineResponseDto> Lines { get; set; }
}
using System.Text.Json.Serialization;
using WorkTest.Models.Dto.OrderLine;
using WorkTest.Models.Enum;

namespace WorkTest.Models.Dto.Order;

public class OrderResponseDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("status")]
    public OrderStatus Status { get; set; }

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("lines")]
    public List<OrderLineResponseDto> Lines { get; set; }
}
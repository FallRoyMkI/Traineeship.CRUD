using System.Text.Json.Serialization;
using WorkTest.Models.Dto.OrderLine;

namespace WorkTest.Models.Dto.Order;

public class OrderAddRequestDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("lines")]
    public List<OrderLineRequestDto> Lines { get; set; }
}
using System.Text.Json.Serialization;
using WorkTest.Models.Dto.Product;
using WorkTest.Models.Enum;

namespace WorkTest.Models.Dto.Order;

public class OrderUpdateRequestDto
{
    [JsonPropertyName("status")]
    public OrderStatus Status { get; set; }

    [JsonPropertyName("lines")]
    public List<ProductResponseDto> Lines { get; set; }
}
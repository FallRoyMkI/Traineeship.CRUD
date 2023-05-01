using System.Text.Json.Serialization;
using WorkTest.Models.Dto.Product;

namespace WorkTest.Models.Dto.Order;

public class OrderAddRequestDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("lines")]
    public List<ProductResponseDto> Lines { get; set; }
}
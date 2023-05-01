using System.Text.Json.Serialization;
using WorkTest.Api.Models.Product.Response;
using WorkTest.Constants;

namespace WorkTest.Models.Dto.Order;

public class OrderResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("status")]
    public OrderStatus Status { get; set; }

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("lines")]
    public List<ProductResponse> Lines { get; set; }
}
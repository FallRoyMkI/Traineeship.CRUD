using WorkTest.Api.Models.Product.Response;
using System.Text.Json.Serialization;
using WorkTest.Constants;

namespace WorkTest.Api.Models.Order.Request;

public class OrderUpdateRequest
{
    [JsonPropertyName("status")]
    public OrderStatus Status { get; set; }

    [JsonPropertyName("lines")]
    public List<ProductResponse> Lines { get; set; }
}
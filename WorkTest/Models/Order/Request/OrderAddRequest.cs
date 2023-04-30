using System.Text.Json.Serialization;
using WorkTest.Api.Models.Product.Response;

namespace WorkTest.Api.Models.Order.Request;

public class OrderAddRequest
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("lines")]
    public List<ProductResponse> Lines { get; set; }
}
using System.Text.Json.Serialization;

namespace WorkTest.Api.Models.Product.Response;

public class ProductResponse
{
    [JsonPropertyName("id")]
    public Guid ProdId { get; set; }

    [JsonPropertyName("qty")]
    public int Qty { get; set; }
}
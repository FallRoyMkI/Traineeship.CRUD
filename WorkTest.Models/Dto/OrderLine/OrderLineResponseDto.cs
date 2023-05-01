using System.Text.Json.Serialization;

namespace WorkTest.Models.Dto.Product;

public class ProductResponseDto
{
    [JsonPropertyName("id")]
    public Guid ProdId { get; set; }

    [JsonPropertyName("qty")]
    public int Qty { get; set; }
}
using System.Text.Json.Serialization;

namespace WorkTest.Models.Dto.OrderLine;

public class OrderLineResponseDto
{
    [JsonPropertyName("id")]
    public Guid ProdId { get; set; }

    [JsonPropertyName("qty")]
    public int Qty { get; set; }
}
using System.Text.Json.Serialization;

namespace WorkTest.Models.Dto.OrderLine;

public class OrderLineRequestDto
{
    [JsonPropertyName("id")]
    public Guid OrderLineId { get; set; }

    [JsonPropertyName("qty")]
    public int Qty { get; set; }
}
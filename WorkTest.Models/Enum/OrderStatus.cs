using System.Text.Json.Serialization;

namespace WorkTest.Models.Enum;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    New = 0,
    WaitingForPayment = 1,
    Paid = 2,
    TransferredForDelivery = 3,
    Delivered = 4,
    Completed = 5
}
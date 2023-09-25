using System.Text.Json.Serialization;

namespace NFT.Application.Domain;

public class Transaction
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TransactionType Type { get; set; }
    public string TokenId { get; set; }
    public string Address { get; set; }
    public string From { get; set; }
    public string To { get; set; }
}
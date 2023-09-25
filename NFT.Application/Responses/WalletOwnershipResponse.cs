namespace NFT.Application.Responses;

public class WalletOwnershipResponse : BaseResponse
{
    public IEnumerable<string> Tokens { get; set; }
}
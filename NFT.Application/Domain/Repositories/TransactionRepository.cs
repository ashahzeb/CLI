using NFT.Application.Storage;

namespace NFT.Application.Domain.Repositories;

public interface ITransactionRepository
{
    Task<string> GetByIdAsync(string tokenId);
    Task<IEnumerable<string>> GetByAddressAsync(string address);
    Task AddAsync(Transaction transaction);
    Task DeleteAsync(Transaction transaction);
    Task UpdateAsync(Transaction transaction);
    Task DeleteAll();
}

public class TransactionRepository : ITransactionRepository
{
    private readonly IOwnershipData _ownershipData;

    public TransactionRepository(IOwnershipData ownershipOwnershipData)
    {
        _ownershipData = ownershipOwnershipData;
    }

    public async Task<string> GetByIdAsync(string tokenId)
    {
        return _ownershipData.Data.ContainsKey(tokenId) ? _ownershipData.Data[tokenId] : string.Empty;
    }

    public async Task<IEnumerable<string>> GetByAddressAsync(string address)
    {
        var tokens = _ownershipData.Data.Where(pair => pair.Value == address)
                .Select(pair => pair.Key);
        
        return tokens.Any() ? tokens : Enumerable.Empty<string>();
    }

    public async Task AddAsync(Transaction transaction)
    {
        _ownershipData.Data[transaction.TokenId] = transaction.Address;
        _ownershipData.SaveData();
    }

    public async Task DeleteAsync(Transaction transaction)
    {
        _ownershipData.Data.Remove(transaction.TokenId);
        _ownershipData.SaveData();
    }

    public async Task UpdateAsync(Transaction transaction)
    {
        _ownershipData.Data[transaction.TokenId] = transaction.To;
        _ownershipData.SaveData();
    }

    public async Task DeleteAll()
    {
        _ownershipData.Data = new Dictionary<string, string>();
        _ownershipData.SaveData();
    }
}
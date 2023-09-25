using System.IO.Enumeration;
using System.Text.Json;

namespace NFT.Application.Storage;

public interface IOwnershipData
{
    Dictionary<string, string> Data { get; set; }
    void SaveData();
}

public class OwnershipData : IOwnershipData
{
    public Dictionary<string, string> Data { get; set; } = new();
    
    private const string FileName = "nft-db.json";

    public OwnershipData()
    {
        LoadData();
    }

    private void LoadData()
    {
        if (File.Exists(FileName))
        {
            string json = File.ReadAllText(FileName);
            Data = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
    }

    public void SaveData()
    {
        string json = JsonSerializer.Serialize(Data);
        File.WriteAllText(FileName, json);
    }
}
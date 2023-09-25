using System.Text.Json;
using System.Text.Json.Nodes;

namespace NFT.Application.Helpers;

public class JsonHelper<T> : IJsonHelper<T>
{
    public bool IsJsonArray(string json)
    {
        try
        {
            JsonNode.Parse(json);
            if (json.Trim().StartsWith("[") && json.Trim().EndsWith("]"))
            {
                return true;
            }
            return false;
        }
        catch (JsonException)
        {
            return false;
        }
    }

    public List<T> ProcessArray(string jsonArray)
    {
        try
        {
            return JsonSerializer.Deserialize<List<T>>(jsonArray)!;
        }
        catch (JsonException)
        {
            throw new ArgumentException("Invalid JSON format.");
        }
    }

    public T? ProcessSingleObject(string jsonObject)
    {
        try
        {
            return JsonSerializer.Deserialize<T>(jsonObject);
        }
        catch (JsonException ex)
        {
            throw new ArgumentException("Invalid JSON format.");
        }
    }
}
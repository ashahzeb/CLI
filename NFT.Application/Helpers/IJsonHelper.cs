namespace NFT.Application.Helpers;

public interface IJsonHelper<T>
{
    bool IsJsonArray(string json);
    List<T> ProcessArray(string jsonArray);
    T ProcessSingleObject(string jsonObject);
}
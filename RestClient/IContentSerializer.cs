namespace RestClient
{
    public interface IContentSerializer
    {
        string Serialize(object content);
        T Deserialize<T>(string content);
    }
}
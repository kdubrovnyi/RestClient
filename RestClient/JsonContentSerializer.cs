using Newtonsoft.Json;

namespace RestClient
{
    public class JsonContentSerializer : IContentSerializer
    {
        public string Serialize(object content)
        {
            return JsonConvert.SerializeObject(content);
        }

        public T Deserialize<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
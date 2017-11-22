using System.Threading.Tasks;

namespace RestClient
{
    public interface IRestClient
    {
        T Get<T>(string uri);
        void Post<T>(string uri, T content);
        void Put<T>(string uri, T content);
        void Delete(string uri);

        Task<TContent> GetAsync<TContent>(string uri);
        Task PostAsync<TContent>(string uri, TContent content);
        Task PutAsync<TContent>(string uri, TContent content);
        Task DeleteAsync(string uri);
    }
}
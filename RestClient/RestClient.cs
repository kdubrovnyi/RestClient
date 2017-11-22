using System;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace RestClient
{
    public class RestClient : IRestClient
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly IContentSerializer _contentSerializer;

        public RestClient() : this(new JsonContentSerializer())
        {
        }

        public RestClient(IContentSerializer contentSerializer)
        {
            _contentSerializer = contentSerializer;
        }

        public TContent Get<TContent>(string uri)
        {
            return GetAsync<TContent>(uri).Result;
        }

        public void Post<TContent>(string uri, TContent content)
        {
            PostAsync(uri, content).Wait();
        }

        public void Put<TContent>(string uri, TContent content)
        {
            PutAsync(uri, content).Wait();
        }

        public void Delete(string uri)
        {
            DeleteAsync(uri).Wait();
        }

        public async Task<TContent> GetAsync<TContent>(string uri)
        {
            var response = await SendAsync(CreateRequest(HttpMethod.Get, uri));
            return await GetResponseContentAsync<TContent>(response);
        }

        public async Task PostAsync<TContent>(string uri, TContent content)
        {
            await SendAsync(CreateRequest(HttpMethod.Post, uri, content));
        }

        public async Task PutAsync<TContent>(string uri, TContent content)
        {
            await SendAsync(CreateRequest(HttpMethod.Put, uri, content));
        }

        public async Task DeleteAsync(string uri)
        {
            await SendAsync(CreateRequest(HttpMethod.Delete, uri));
        }

        private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);
            return EnsureSuccessStatusCode(response);
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string uri, object body)
        {
            return new HttpRequestMessage(method, uri)
            {
                Content = new StringContent(_contentSerializer.Serialize(body))
            };
        }

        private static HttpRequestMessage CreateRequest(HttpMethod method, string uri)
        {
            return new HttpRequestMessage(method, uri);
        }

        private async Task<TContent> GetResponseContentAsync<TContent>(HttpResponseMessage response)
        {
            var contentString = await response.Content.ReadAsStringAsync();
            return _contentSerializer.Deserialize<TContent>(contentString);
        }

        private static HttpResponseMessage EnsureSuccessStatusCode(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new RestHttpException(response.StatusCode, response.ReasonPhrase);

            return response;
        }
    }
}

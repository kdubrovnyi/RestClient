using System.Net;
using System.Net.Http;

namespace RestClient
{
    public class RestHttpException : HttpRequestException
    {
        public RestHttpException(HttpStatusCode statusCode, string reasonPhrase)
        {
            StatusCode = statusCode;
            ReasonPhrase = reasonPhrase;
        }

        public HttpStatusCode StatusCode { get; }
        public string ReasonPhrase { get; }
    }
}
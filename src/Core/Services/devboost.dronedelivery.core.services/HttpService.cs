using devboost.dronedelivery.core.domain.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace devboost.dronedelivery.core.services
{
    public class HttpService : IHttpHandler
    {

        private readonly HttpClient _client;

        public HttpService()
        {

        }

        public HttpService(bool useHandler = false)
        {
            if (!useHandler)
            {
                _client = new HttpClient();
            } else
            {
                var httpClientHandler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
                };
                _client = new HttpClient(httpClientHandler, false);
            }
        }
        public virtual async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return await _client.PostAsync(url, content);
        }

        public virtual void SetBaseAddress(string url)
        {
            _client.BaseAddress = new Uri(url);
        }
    }
}

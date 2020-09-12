using System.Net.Http;
using System.Threading.Tasks;

namespace devboost.dronedelivery.core.domain.Interfaces
{
    public interface IHttpHandler
    {

        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
        void SetBaseAddress(string url);

    }
}

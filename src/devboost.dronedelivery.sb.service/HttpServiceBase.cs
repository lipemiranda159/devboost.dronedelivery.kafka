using Microsoft.Extensions.Configuration;

namespace devboost.dronedelivery.sb.service
{
    public class HttpServiceBase
    {
        protected readonly string _urlPedidos;
        public HttpServiceBase(IConfiguration configuration)
        {
            _urlPedidos = configuration.GetSection("UrlPedidos").Value;
        }

    }
}

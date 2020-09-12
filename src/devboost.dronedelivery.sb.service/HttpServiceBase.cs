using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace devboost.dronedelivery.sb.service
{
    public class HttpServiceBase
    {
        protected readonly string _urlPedidos;
        public HttpServiceBase(IConfiguration configuration)
        {
            _urlPedidos = configuration.GetSection("UrlPedidos").ToString();
        }

    }
}

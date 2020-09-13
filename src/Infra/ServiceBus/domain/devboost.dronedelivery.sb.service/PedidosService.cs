using devboost.dronedelivery.sb.domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace devboost.dronedelivery.sb.service
{
    public class PedidosService : HttpServiceBase, IPedidosService
    {
        private const string MediaType = "application/json";
        private const string PedidoUri = "api/Pedidos";
        private const string AuthorizationType = "Bearer";

        public PedidosService(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task ProcessPedidoAsync(string token, string pedido)
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri(_urlPedidos)
            };
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthorizationType, token);
            var request = new StringContent(pedido, Encoding.UTF8, MediaType);
            var response = await httpClient.PostAsync(PedidoUri, request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error to post pedido");
            }
        }
    }
}

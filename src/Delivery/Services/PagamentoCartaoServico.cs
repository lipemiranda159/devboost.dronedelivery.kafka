using devboost.dronedelivery.core.domain;
using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.core.domain.Enums;
using devboost.dronedelivery.core.domain.Extensions;
using devboost.dronedelivery.core.domain.Interfaces;
using devboost.dronedelivery.domain.Constants;
using devboost.dronedelivery.domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace devboost.dronedelivery.Services
{
    public class PagamentoCartaoServico : IPagamentoServico
    {
        private const string MEDIA_TYPE = "application/json";
        private readonly string _urlBase;
        private readonly IHttpHandler _client;


        public PagamentoCartaoServico(PaymentSettings paymentSettings, IHttpHandler client)
        {
            _urlBase = paymentSettings.GetUrlBase(ETipoPagamento.CARTAO);
            _client = client;

        }

        public async Task<Pagamento> RequisitaPagamento(Pagamento pagamento)
        {
            _client.SetBaseAddress(_urlBase);

            var request = JsonConvert.SerializeObject(pagamento.ToPagamentoCreate());
            var httpContent = new StringContent(request, Encoding.UTF8, MEDIA_TYPE);
            var gatewayResponse = await _client.PostAsync(ProjectConsts.PAGAMENTO_URI, httpContent);
            if (gatewayResponse.IsSuccessStatusCode)
            {
                var response = await gatewayResponse.Content.ReadAsStringAsync();
                return JSONHelper.DeserializeJsonToObject<Pagamento>(response);
            }
            else throw new Exception("Falha no pagamento");
        }
    }
}

using devboost.dronedelivery.core.domain;
using devboost.dronedelivery.core.services;
using devboost.dronedelivery.domain.Constants;
using devboost.dronedelivery.domain.Interfaces;
using devboost.dronedelivery.pagamento.EF.Integration.Interfaces;
using devboost.dronedelivery.Services;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace devboost.dronedelivery.pagamento.EF.Integration
{
    public class PagamentoIntegration : IPagamentoIntegration
    {
        private readonly string _urlBase;
        private readonly HttpService _httpService;
        public PagamentoIntegration(DeliverySettingsData deliverySettings, HttpService httpService)
        {
            _urlBase = deliverySettings.UrlBase;
            _httpService = httpService;
        }

        public async Task<bool> ReportarResultadoAnalise(List<PagamentoStatusDto> listaPagamentos)
        {
            string finalUri = string.Concat(_urlBase, ProjectConsts.DELIVERY_URI);

            var apiDeliveryResponse = await _httpService.PostAsync(finalUri, JSONHelper.ConvertObjectToByteArrayContent<List<PagamentoStatusDto>>(listaPagamentos));

            return apiDeliveryResponse.IsSuccessStatusCode;

        }
    }
}

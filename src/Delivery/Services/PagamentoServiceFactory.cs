using devboost.dronedelivery.core.domain;
using devboost.dronedelivery.core.domain.Enums;
using devboost.dronedelivery.core.domain.Interfaces;
using devboost.dronedelivery.domain.Interfaces;
using System;

namespace devboost.dronedelivery.Services
{
    public class PagamentoServiceFactory : IPagamentoServiceFactory
    {
        private readonly PaymentSettings _paymentSettings;
        private readonly IHttpHandler _httpHandler;
        public PagamentoServiceFactory(PaymentSettings payment, IHttpHandler httpHandler)
        {
            _paymentSettings = payment;
            _httpHandler = httpHandler;
        }
        public IPagamentoServico GetPagamentoServico(ETipoPagamento tipoPagamento)
        {
            switch (tipoPagamento)
            {
                case ETipoPagamento.CARTAO:
                    return new PagamentoCartaoServico(_paymentSettings, _httpHandler);
                default: throw new NotImplementedException("Servico não implementado!");
            }
        }
    }
}
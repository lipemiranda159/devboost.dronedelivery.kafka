using System;
using System.Collections.Generic;
using System.Text;
using devboost.dronedelivery.core.domain;
using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.core.domain.Enums;
using devboost.dronedelivery.pagamento.Api.Controllers;
using devboost.dronedelivery.pagamento.domain.Interfaces;
using NSubstitute;

namespace devboost.Dronedelivery.Pagamento.Test.Pagamentos
{
    public class PagamentoAtualizaListaTest
    {
        private readonly List<PagamentoStatusDto> _listPagamentoStatusDto;
        private readonly IPagamentoFacade _pagamentoFacade;

        public PagamentoAtualizaListaTest()
        {
            dronedelivery.core.domain.Entities.Pagamento pagamento = new dronedelivery.core.domain.Entities.Pagamento
            {
                Id = 0,
                DataCriacao = DateTime.Now,
                Descricao = "Pagamento Teste",
                TipoPagamento = ETipoPagamento.CARTAO,
                StatusPagamento = EStatusPagamento.EM_ANALISE,
                DadosPagamentos = new List<DadosPagamento> { new DadosPagamento { Id = 0, Dados = "4220456798763234" } }

            };


            var _pagamentoStatusDto = new PagamentoStatusDto
            {
                IdPagamento = pagamento.Id,
                Status = EStatusPagamento.APROVADO,
                Descricao = pagamento.Descricao
            };

            _listPagamentoStatusDto = new List<PagamentoStatusDto> { _pagamentoStatusDto };

            _pagamentoFacade = Substitute.For<IPagamentoFacade>();

        }
    }
}

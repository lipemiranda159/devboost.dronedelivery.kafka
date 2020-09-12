using devboost.dronedelivery.core.domain;
using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.core.domain.Enums;
using devboost.dronedelivery.pagamento.domain.Interfaces;
using devboost.dronedelivery.pagamento.EF.Integration.Interfaces;
using devboost.dronedelivery.pagamento.EF.Repositories.Interfaces;
using devboost.dronedelivery.pagamento.services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace devboost.Dronedelivery.Pagamento.Test
{
    public class PagamentoControllerTest
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IPagamentoIntegration _pagamentoIntegration;
        private readonly IPagamentoFacade _pagamentofacade;
        private PagamentoStatusDto _pagamentoStatusDto;
        private List<PagamentoStatusDto> _listPagamentoStatusDto;

        public PagamentoControllerTest()
        {
            var pagamento = new dronedelivery.core.domain.Entities.Pagamento
            {
                Id = 0,
                DataCriacao = DateTime.Now,
                Descricao = "Pagamento Teste",
                TipoPagamento = ETipoPagamento.CARTAO,
                StatusPagamento = EStatusPagamento.EM_ANALISE,
                DadosPagamentos = new System.Collections.Generic.List<DadosPagamento> { new DadosPagamento { Id = 0, Dados = "4220456798763234" } }

            };

            _pagamentoRepository = Substitute.For<IPagamentoRepository>();
            _pagamentoRepository.GetPagamentosEmAnaliseAsync().Returns(new List<dronedelivery.core.domain.Entities.Pagamento> { pagamento });


            var pagamentoStatusDto = new PagamentoStatusDto
            {
                IdPagamento = pagamento.Id,
                Status = EStatusPagamento.APROVADO,
                Descricao = pagamento.Descricao
            };

            _listPagamentoStatusDto = new List<PagamentoStatusDto> { pagamentoStatusDto };

            _pagamentoIntegration = Substitute.For<IPagamentoIntegration>();
            _pagamentoIntegration.ReportarResultadoAnalise(Arg.Is<List<PagamentoStatusDto>>(x => x.First().IdPagamento == 0)).Returns(true);

            _pagamentofacade = new PagamentoFacade(_pagamentoRepository, _pagamentoIntegration);

        }

        [Fact]
        public void VerificarStatusPagamento()
        {
            PagamentoFacade pagamentoFacade = new PagamentoFacade(_pagamentoRepository, _pagamentoIntegration);

            var pagamentoStatus = pagamentoFacade.VerificarStatusPagamentos().Result;

            Assert.True(pagamentoStatus.First().Status == EStatusPagamento.APROVADO);
        }

        [Fact]
        public void CriarPagamento()
        {
            PagamentoCreateDto pagamentoDto = new PagamentoCreateDto
            {
                Descricao = "Teste Pagamento Dto",
                DadosPagamentos = new List<DadosPagamento> { new DadosPagamento { Id = 0, Dados = "4220456798763234" } },
                TipoPagamento = ETipoPagamento.CARTAO
            };

            var pagamento = _pagamentofacade.CriarPagamento(pagamentoDto).Result;

            Assert.NotNull(pagamento);

        }

        [Fact]
        public async void PagamentoIntegration()
        {
            var resultTest = await _pagamentoIntegration.ReportarResultadoAnalise(_listPagamentoStatusDto);

            Assert.True(resultTest);
        }
    }
}

using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.core.domain.Enums;
using devboost.dronedelivery.pagamento.EF;
using devboost.dronedelivery.pagamento.EF.Repositories;
using devboost.dronedelivery.pagamento.EF.Repositories.Interfaces;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace devboost.Dronedelivery.Pagamento.Test.Pagamentos
{
    public class PagamentoRepositoryTest
    {
        private readonly DataContext _context;
        private readonly IPagamentoRepository _pagamentoRepository;
        private new dronedelivery.core.domain.Entities.Pagamento _pagamento;

        public PagamentoRepositoryTest()
        {
            _context = Substitute.For<DataContext>();

            _context.Pagamento.Add(_pagamento);

            _pagamentoRepository = new PagamentoRepository(_context);

            _pagamento = new dronedelivery.core.domain.Entities.Pagamento
            {
                Id = 0,
                DataCriacao = DateTime.Now,
                Descricao = "Pagamento Teste",
                TipoPagamento = ETipoPagamento.CARTAO,
                StatusPagamento = EStatusPagamento.EM_ANALISE,
                DadosPagamentos = new List<DadosPagamento> { new DadosPagamento { Id = 0, Dados = "4220456798763234" } }

            };

        }

        [Fact]
        public async Task PagamentoEmAnalise()
        {
            IPagamentoRepository pagamentoRepository = Substitute.For<IPagamentoRepository>();
            pagamentoRepository.GetPagamentosEmAnaliseAsync().Returns(new List<dronedelivery.core.domain.Entities.Pagamento> { _pagamento });

            var listPagamentos = await pagamentoRepository.GetPagamentosEmAnaliseAsync();

            Assert.True(listPagamentos.Count == 1);
            Assert.True(listPagamentos.First().StatusPagamento == EStatusPagamento.EM_ANALISE);
        }

    }
}

using devboost.dronedelivery.core.domain.Extensions;
using Xunit;

namespace devboost.dronedelivery.test.DTO.Extensions
{
    public class PagamentoExtensionsTests
    {


        [Fact]
        public void ToPagamentoCreate()
        {

            var pagamento = SetupTests.GetPagamento();

            var pagamentoDto = pagamento.ToPagamentoCreate();

            Assert.Equal(pagamento.Descricao, pagamentoDto.Descricao);
            Assert.Equal(pagamento.TipoPagamento.ToString(), pagamentoDto.TipoPagamento.ToString());
            Assert.Equal(pagamento.DadosPagamentos.Count, pagamentoDto.DadosPagamentos.Count);

        }

    }
}

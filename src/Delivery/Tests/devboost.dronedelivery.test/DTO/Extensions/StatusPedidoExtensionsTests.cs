using devboost.dronedelivery.core.domain.Enums;
using devboost.dronedelivery.core.domain.Extensions;
using Xunit;

namespace devboost.dronedelivery.test.DTO.Extensions
{
    public class StatusPedidoExtensionsTests
    {

        [Fact]
        public void isSuccess()
        {

            var pagamento = SetupTests.GetPagamento();
            pagamento.StatusPagamento = EStatusPagamento.APROVADO;

            var status = pagamento.StatusPagamento.IsSuccess();

            Assert.True(status);

        }

        [Fact]
        public void EmAnalise()
        {
            var pagamento = SetupTests.GetPagamento();
            pagamento.StatusPagamento = EStatusPagamento.EM_ANALISE;

            var status = pagamento.StatusPagamento.EmAnalise();

            Assert.True(status);
        }

    }
}

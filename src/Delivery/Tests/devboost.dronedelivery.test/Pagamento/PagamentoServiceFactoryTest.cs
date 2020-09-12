using devboost.dronedelivery.core.domain.Enums;
using devboost.dronedelivery.core.domain.Interfaces;
using devboost.dronedelivery.domain.Interfaces;

using devboost.dronedelivery.Services;
using NSubstitute;
using Xunit;

namespace devboost.dronedelivery.test
{
    public class PagamentoServiceFactoryTest
    {

        [Fact]
        public void GetPagamentoServicoTest()
        {
            var client = Substitute.For<IHttpHandler>();
            var pagamentoServiceFactory = new PagamentoServiceFactory(SetupTests.GetPaymentSettings(), client);

            var service = pagamentoServiceFactory.GetPagamentoServico(ETipoPagamento.CARTAO);

            Assert.NotNull(service);
            Assert.IsAssignableFrom<IPagamentoServico>(service);

        }

    }
}

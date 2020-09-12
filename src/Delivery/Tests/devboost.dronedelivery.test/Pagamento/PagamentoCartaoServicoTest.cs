using devboost.dronedelivery.core.domain.Extensions;
using devboost.dronedelivery.core.domain.Interfaces;
using devboost.dronedelivery.domain.Interfaces;
using devboost.dronedelivery.Services;
using NSubstitute;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace devboost.dronedelivery.test
{
    public class PagamentoCartaoServicoTest
    {
        private readonly IPagamentoServico _pagamentoServico;

        public PagamentoCartaoServicoTest()
        {
            _pagamentoServico = Substitute.For<IPagamentoServico>();
        }

        [Fact]
        public async Task RequisitaPagamentoTest()
        {
            var pagamento = SetupTests.GetPagamento();
            var client = Substitute.For<IHttpHandler>();
            var pgtoCartaoServico = new PagamentoCartaoServico(SetupTests.GetPaymentSettings(), client);


            var request = JsonSerializer.Serialize(pagamento.ToPagamentoCreate());
            var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
            var response = new HttpResponseMessage() { StatusCode = HttpStatusCode.OK, Content = httpContent };

            client.PostAsync(Arg.Any<string>(), Arg.Any<HttpContent>()).Returns(response);

            var result = await pgtoCartaoServico.RequisitaPagamento(pagamento);

            Assert.NotNull(pagamento);

        }

    }
}

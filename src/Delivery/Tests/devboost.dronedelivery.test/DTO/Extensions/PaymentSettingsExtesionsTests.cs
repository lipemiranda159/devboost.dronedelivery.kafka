using Castle.DynamicProxy.Internal;
using devboost.dronedelivery.core.domain.Enums;
using devboost.dronedelivery.domain.Extensions;
using Xunit;

namespace devboost.dronedelivery.test.DTO.Extensions
{
    public class PaymentSettingsExtesionsTests
    {

        [Fact]
        public void GetUrlBase()
        {

            var paymentSettings = SetupTests.GetPaymentSettings();

            var urlBase = paymentSettings.GetUrlBase(ETipoPagamento.CARTAO);

            Assert.IsType(TypeUtil.GetTypeOrNull(""), urlBase);

        }

    }
}

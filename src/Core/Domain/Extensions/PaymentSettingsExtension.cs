using devboost.dronedelivery.core.domain.Enums;
using System.Linq;

namespace devboost.dronedelivery.core.domain.Extensions
{
    public static class PaymentSettingsExtension
    {
        public static string GetUrlBase(this PaymentSettings paymentSettings, ETipoPagamento tipoPagamento)
        {
            return paymentSettings.PaymentsSettings.Where(FiltraTipoPagamento(tipoPagamento)).FirstOrDefault().UrlBase;
        }

        private static System.Func<PaymentSetting, bool> FiltraTipoPagamento(ETipoPagamento tipoPagamento)
        {
            return p => p.TipoPagamento == (int)tipoPagamento;
        }

    }
}

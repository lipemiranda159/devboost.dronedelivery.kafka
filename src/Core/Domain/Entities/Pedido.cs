using System;
using System.ComponentModel.DataAnnotations;

namespace devboost.dronedelivery.core.domain.Entities
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public int Peso { get; set; }

        public DateTime DataHoraInclusao { get; set; }

        public int Situacao { get; set; }
        public Pagamento Pagamento { get; set; }
        public int PagamentoId { get; set; }
        public string GatewayPagamentoId { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }
        public DateTime DataHoraFinalizacao { get; set; }

        public bool EValido()
        {
            return Pagamento != null && Pagamento.ContemFormaDePagamento();
        }
    }
}

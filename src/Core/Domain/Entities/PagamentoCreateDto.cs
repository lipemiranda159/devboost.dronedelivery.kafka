using devboost.dronedelivery.core.domain.Enums;
using System.Collections.Generic;

namespace devboost.dronedelivery.core.domain.Entities
{
    public class PagamentoCreateDto
    {
        public List<DadosPagamento> DadosPagamentos { get; set; }
        public ETipoPagamento TipoPagamento { get; set; }

        public string Descricao { get; set; }

        public PagamentoCreateDto()
        {
            DadosPagamentos = new List<DadosPagamento>();
        }
    }
}

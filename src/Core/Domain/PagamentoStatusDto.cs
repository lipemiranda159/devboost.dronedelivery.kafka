using devboost.dronedelivery.core.domain.Enums;

namespace devboost.dronedelivery.core.domain
{
    public class PagamentoStatusDto
    {
        public int IdPagamento { get; set; }
        public EStatusPagamento Status { get; set; }
        public string Descricao { get; set; }
    }
}

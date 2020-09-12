using devboost.dronedelivery.core.domain;
using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.domain.Entities;
using devboost.dronedelivery.domain.Enums;
using devboost.dronedelivery.domain.Interfaces;
using devboost.dronedelivery.domain.Interfaces.Repositories;
using devboost.dronedelivery.Infra.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace devboost.dronedelivery.felipe.EF.Repositories
{

    public class DroneRepository : RepositoryBase<Drone>, IDroneRepository
    {
        private readonly ICommandExecutor<DroneStatusResult> _droneStatusExecutor;
        private readonly ICommandExecutor<StatusDroneDto> _statusDroneExecutor;


        public DroneRepository(DataContext context, ICommandExecutor<StatusDroneDto> statusDroneExecutor,
            ICommandExecutor<DroneStatusResult> droneStatusExecutor) : base(context)
        {
            _droneStatusExecutor = droneStatusExecutor;
            _statusDroneExecutor = statusDroneExecutor;
        }


        public Drone RetornaDrone()
        {
            return Context.Drone.FirstOrDefault();
        }

        public List<StatusDroneDto> GetDroneStatus()
        {
            return _statusDroneExecutor.ExcecuteCommand(GetStatusSqlCommand()).ToList();
        }


        public DroneStatusDto RetornaDroneStatus(int droneId)
        {
            var consulta = _droneStatusExecutor.ExcecuteCommand(GetSqlCommand(droneId)).ToList();
            if (consulta.Any())
            {
                var droneData = consulta.FirstOrDefault();
                return new DroneStatusDto
                {
                    Drone = new Drone()
                    {
                        Id = droneData.Id,
                        Velocidade = droneData.Velocidade,
                        Capacidade = droneData.Capacidade,
                        Autonomia = droneData.Autonomia,
                        Carga = droneData.Carga,
                        Perfomance = droneData.Perfomance,
                    },
                    SomaPeso = droneData.SomaPeso,
                    SomaDistancia = droneData.SomaDistancia,

                };
            }
            return new DroneStatusDto();
        }

        private string GetSelectPedidos(int situacao, StatusEnvio status)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("select a.DroneId,");
            stringBuilder.AppendLine($"{situacao} as Situacao,");
            stringBuilder.AppendLine(" a.Id as PedidoId,");
            stringBuilder.AppendLine(" c.Id as ClienteId,");
            stringBuilder.AppendLine(" c.Nome,");
            stringBuilder.AppendLine(" c.Latitude,");
            stringBuilder.AppendLine(" c.Longitude");
            stringBuilder.AppendLine(" from PedidoDrones a,");
            stringBuilder.AppendLine(" Pedido b,");
            stringBuilder.AppendLine(" Cliente c");
            stringBuilder.AppendLine($" where a.StatusEnvio <> {(int)status}");
            stringBuilder.AppendLine(" and a.DataHoraFinalizacao > dateadd(hour,-3,CURRENT_TIMESTAMP)");
            stringBuilder.AppendLine(" and a.PedidoId = b.ID");
            stringBuilder.AppendLine(" and b.ClienteId = c.Id");

            return stringBuilder.ToString();
        }

        private string GetStatusSqlCommand()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(GetSelectPedidos(0, StatusEnvio.AGUARDANDO));
            stringBuilder.AppendLine(" union");
            stringBuilder.Append(GetSelectPedidos(1, StatusEnvio.EM_TRANSITO));
            stringBuilder.AppendLine(" union");
            stringBuilder.AppendLine(" select b.Id as DroneId,");
            stringBuilder.AppendLine(" 1 as Situacao,");
            stringBuilder.AppendLine(" 0 as PedidoId,");
            stringBuilder.AppendLine(" 0 as ClienteId,");
            stringBuilder.AppendLine(" ' ' as Nome,");
            stringBuilder.AppendLine(" 0 as Latitude,");
            stringBuilder.AppendLine(" 0 as Longitude");
            stringBuilder.AppendLine(" from  Drone b");
            stringBuilder.AppendLine(" where b.Id NOT IN  (");
            stringBuilder.AppendLine(" select a.DroneId");
            stringBuilder.AppendLine(" from PedidoDrones a");
            stringBuilder.AppendLine($" where a.StatusEnvio <> {(int)StatusEnvio.FINALIZADO}");
            stringBuilder.AppendLine(" and a.DataHoraFinalizacao > dateadd(hour,-3,CURRENT_TIMESTAMP)");
            stringBuilder.AppendLine(")");

            return stringBuilder.ToString();
        }

        private static string GetSqlCommand(int droneId)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT D.*,");
            stringBuilder.AppendLine("SUM(P.Peso) AS SomaPeso,");
            stringBuilder.AppendLine("SUM(PD.Distancia) AS SomaDistancia ");
            stringBuilder.AppendLine("FROM dbo.PedidoDrones PD ");
            stringBuilder.AppendLine("JOIN dbo.Drone D");
            stringBuilder.AppendLine("on PD.DroneId = D.Id");
            stringBuilder.AppendLine("JOIN dbo.Pedido P");
            stringBuilder.AppendLine("on PD.PedidoId = P.Id");
            stringBuilder.AppendLine($"WHERE PD.DroneId = {droneId}");
            stringBuilder.AppendLine("GROUP BY D.Id, D.Autonomia, D.Capacidade, D.Carga, D.Perfomance, D.Velocidade");

            return stringBuilder.ToString();
        }
    }
}

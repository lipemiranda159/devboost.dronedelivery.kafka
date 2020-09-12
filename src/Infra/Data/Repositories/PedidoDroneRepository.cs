using devboost.dronedelivery.core.domain;
using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.core.domain.Extensions;
using devboost.dronedelivery.domain.Enums;
using devboost.dronedelivery.domain.Interfaces;
using devboost.dronedelivery.domain.Interfaces.Repositories;
using devboost.dronedelivery.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace devboost.dronedelivery.felipe.EF.Repositories
{
    public class PedidoDroneRepository : RepositoryBase<PedidoDrone>, IPedidoDroneRepository
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IDroneRepository _droneRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly ICommandExecutor<PedidoDrone> _commandExecutor;

        public PedidoDroneRepository(DataContext context, IPedidoRepository pedidoRepository,
            IDroneRepository droneRepository,
            IClienteRepository clienteRepository,
            ICommandExecutor<PedidoDrone> commandExecutor) : base(context)
        {
            _pedidoRepository = pedidoRepository;
            _droneRepository = droneRepository;
            _clienteRepository = clienteRepository;
            _commandExecutor = commandExecutor;

        }

        public async Task<List<PedidoDrone>> RetornaPedidosEmAbertoAsync()
        {
            List<PedidoDrone> pedidoDrones = new List<PedidoDrone>();

            var busca = await Context.PedidoDrones.Where(FiltroPedidosEmAberto()).ToListAsync();

            if (busca.Count > 0)
            {
                foreach (var b in busca)
                {
                    var pedido = await _pedidoRepository.GetByIdAsync(b.PedidoId);
                    pedido.Cliente = await _clienteRepository.GetByIdAsync(pedido.ClienteId);

                    var pedidoDrone = new PedidoDrone
                    {
                        Id = b.Id,
                        DataHoraFinalizacao = b.DataHoraFinalizacao,
                        DroneId = b.DroneId,
                        PedidoId = b.PedidoId,
                        Distancia = b.Distancia,
                        StatusEnvio = b.StatusEnvio,
                        Pedido = pedido,
                        Drone = await _droneRepository.GetByIdAsync(b.DroneId),
                    };

                    pedidoDrones.Add(pedidoDrone);
                }
            }

            return pedidoDrones;
        }

        private static Expression<Func<PedidoDrone, bool>> FiltroPedidosEmAberto()
        {
            return p => p.StatusEnvio == (int)StatusEnvio.AGUARDANDO;
        }

        public async Task UpdatePedidoDroneAsync(DroneStatusDto drone, double distancia)
        {

            var sql = "UPDATE dbo.PedidoDrones" +
                $" SET StatusEnvio ={(int)StatusEnvio.EM_TRANSITO}," +
                $"DataHoraFinalizacao = '{drone.Drone.ToTempoGasto(distancia)}'" +
                $" WHERE DroneId = {drone.Drone.Id}";
            await _commandExecutor.ExecuteCommandAsync(sql);

        }

        public async Task<List<PedidoDrone>> RetornaPedidosParaFecharAsync()
        {
            return await Context
                .PedidoDrones
                .Where(p =>
                    p.StatusEnvio == (int)StatusEnvio.EM_TRANSITO &&
                    p.DataHoraFinalizacao <= DateTime.Now)
                .ToListAsync();
        }

    }
}

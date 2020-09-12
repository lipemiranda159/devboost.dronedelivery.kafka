using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.domain.Entities;
using devboost.dronedelivery.domain.Enums;
using devboost.dronedelivery.domain.Interfaces;
using devboost.dronedelivery.domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace devboost.dronedelivery.felipe.Facade
{
    public class PedidoFacade : IPedidoFacade
    {
        private readonly IPedidoService _pedidoService;
        private readonly IClienteRepository _clienteRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IDroneRepository _droneRepository;
        private readonly IPagamentoServiceFactory _pagamentoServiceFactory;
        private readonly IPedidoDroneRepository _pedidoDroneRepository;
        public PedidoFacade(
            IPedidoService pedidoFacade,
            IClienteRepository clienteRepository,
            IPedidoRepository pedidoRepository,
            IDroneRepository droneRepository,
            IPagamentoServiceFactory pagamentoServiceFactory,
            IPedidoDroneRepository pedidoDroneRepository)
        {
            _pedidoService = pedidoFacade;
            _clienteRepository = clienteRepository;
            _pedidoRepository = pedidoRepository;
            _droneRepository = droneRepository;
            _pagamentoServiceFactory = pagamentoServiceFactory;
            _pedidoDroneRepository = pedidoDroneRepository;
        }
        public async Task AssignDroneAsync()
        {
            var pedidos = _pedidoRepository.ObterPedidos((int)StatusPedido.AGUARDANDO);
            if (pedidos?.Count > 0)
            {
                foreach (var pedido in pedidos)
                {
                    pedido.Cliente = await _clienteRepository.GetByIdAsync(pedido.ClienteId);
                    var drone = await _pedidoService.DroneAtendePedido(pedido);
                    if (drone != null)
                    {
                        await AtualizaPedidoAsync(pedido);
                        await AdicionarPedidoDrone(pedido, drone);
                    }
                }
            }
        }

        private async Task AdicionarPedidoDrone(Pedido pedido, DroneDto drone)
        {
            var newDrone = await _droneRepository.GetByIdAsync(drone.DroneStatus.Drone.Id);
            var newPedido = await _pedidoRepository.GetByIdAsync(pedido.Id);

            var pedidoDrone = new PedidoDrone()
            {
                Distancia = drone.Distancia,
                Drone = newDrone,
                DroneId = drone.DroneStatus.Drone.Id,
                Pedido = newPedido,
                PedidoId = pedido.Id,
                StatusEnvio = (int)StatusEnvio.AGUARDANDO
            };

            await _pedidoDroneRepository.AddAsync(pedidoDrone);
        }

        private async Task AtualizaPedidoAsync(Pedido pedido)
        {
            pedido.Situacao = (int)StatusPedido.AGUARDANDO_ENVIO;
            pedido.DataUltimaAlteracao = DateTime.Now;
            await _pedidoRepository.UpdateAsync(pedido);
        }

        public async Task<IEnumerable<Pedido>> ObterTodosPedidos()
        {
            return await _pedidoRepository.ObterTodosPedidos();
        }

        public async Task<Pedido> CreatePedidoAsync(Pedido pedido)
        {
            if (pedido.EValido())
            {
                pedido.Cliente = await _clienteRepository.GetByIdAsync(pedido.ClienteId);
                pedido.DataHoraInclusao = DateTime.Now;
                pedido.Situacao = (int)StatusPedido.AGUARDANDO_PAGAMENTO;
                var servicoPagamento = _pagamentoServiceFactory.GetPagamentoServico(pedido.Pagamento.TipoPagamento);
                var responseGateway = await servicoPagamento.RequisitaPagamento(pedido.Pagamento);
                pedido.GatewayPagamentoId = responseGateway.Id.ToString();

                await _pedidoRepository.AddAsync(pedido);
                return pedido;
            }
            else
            {
                throw new Exception("Pedido inválido");
            }
        }
    }
}

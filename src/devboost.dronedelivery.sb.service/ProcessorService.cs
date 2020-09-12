using devboost.dronedelivery.sb.domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace devboost.dronedelivery.sb.service
{
    public class ProcessorService : IProcessorQueue
    {
        private readonly IConsumer _consumer;
        private readonly ILoginProvider _loginProvider;
        private readonly IPedidosService _pedidoService;
        public ProcessorService(IConsumer consumer, ILoginProvider loginProvider, IPedidosService pedidosService)
        {
            _consumer = consumer;
            _loginProvider = loginProvider;
            _pedidoService = pedidosService;

        }
        public async Task ProcessorQueueAsync()
        {
            using var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            var messages = await _consumer.ExecuteAsync(cancellationToken.Token, "pedido");
            var token = await _loginProvider.GetTokenAsync();
            foreach (var message in messages)
            {
                await _pedidoService.ProcessPedidoAsync(token, message);
            }
        }
    }
}

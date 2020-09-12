using devboost.dronedelivery.sb.domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace devboost.dronedelivery.sb.service
{
    public class ProcessorService : IProcessorQueue
    {
        private readonly IConsumer _consumer;
        public ProcessorService(IConsumer consumer)
        {
            _consumer = consumer;
        }
        public async Task ProcessorQueueAsync()
        {
            using var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            var messages = await _consumer.ExecuteAsync(cancellationToken.Token, "pedido");
        }
    }
}

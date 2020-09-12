using devboost.dronedelivery.sb.domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace devboost.dronedelivery.sb.service
{
    public class ConsumerService: ServiceBase, IConsumer
    {
        public ConsumerService(string topicName) : base(topicName)
        {
        }

        public Task ExecuteAsync(CancellationToken stopingToken, string topic)
        {
            throw new NotImplementedException();
        }
    }
}

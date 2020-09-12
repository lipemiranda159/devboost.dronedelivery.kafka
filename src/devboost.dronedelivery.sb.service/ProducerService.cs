using Confluent.Kafka;
using devboost.dronedelivery.sb.domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace devboost.dronedelivery.sb.service
{
    public class ProducerService<T> : ServiceBase, IProducer<T>
    {
        public ProducerService(string topicName, IConfiguration configuration) : base(topicName, configuration)
        {
        }

        public Task<DeliveryResult<Null, string>> SendData(string topic, T message)
        {
            throw new NotImplementedException();
        }
    }
}

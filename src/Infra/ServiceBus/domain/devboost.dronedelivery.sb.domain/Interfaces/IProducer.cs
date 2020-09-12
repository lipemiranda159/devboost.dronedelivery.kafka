using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace devboost.dronedelivery.sb.domain.Interfaces
{
    public interface IProducer<T> where T : class
    {
        Task<DeliveryResult<Null, string>> SendData(string topic, T message);
    }
}

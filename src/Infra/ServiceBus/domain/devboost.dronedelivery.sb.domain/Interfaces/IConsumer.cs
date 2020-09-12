using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace devboost.dronedelivery.sb.domain.Interfaces
{
    public interface IConsumer
    {
        Task<List<string>> ExecuteAsync(CancellationToken stopingToken, string topic, string topicName);

    }
}

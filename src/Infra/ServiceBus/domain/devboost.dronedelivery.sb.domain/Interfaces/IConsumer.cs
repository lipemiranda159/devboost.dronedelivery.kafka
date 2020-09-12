using System;
using System.Threading;
using System.Threading.Tasks;

namespace devboost.dronedelivery.sb.domain.Interfaces
{
    public interface IConsumer
    {
        Task ExecuteAsync(CancellationToken stopingToken, string topic);

    }
}

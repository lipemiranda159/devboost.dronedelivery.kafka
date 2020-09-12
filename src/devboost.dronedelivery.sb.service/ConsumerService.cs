using Confluent.Kafka;
using devboost.dronedelivery.sb.domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace devboost.dronedelivery.sb.service
{
    public class ConsumerService: ServiceBase, IConsumer
    {
        public ConsumerService(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<string>> ExecuteAsync(CancellationToken stopingToken, string topic, string topicName)
        {
            var result = new List<string>();

            var config = new ConsumerConfig
            {
                BootstrapServers = _kafcaConnection,
                GroupId = $"{topicName}-group-0",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
                consumer.Subscribe(topicName);

                try
                {
                    var message = "IniciaProcesso";
                    while (!string.IsNullOrEmpty(message))
                    {
                        result.Add(message);
                        var cr = consumer.Consume(cts.Token);
                        message = cr.Message.Value;
                    }
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                }
            }
            catch (Exception ex)
            {
            }

            return result;
        }
    }
}

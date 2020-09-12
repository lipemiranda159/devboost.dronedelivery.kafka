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
        private const string StartProcess = "IniciaProcesso";

        public ConsumerService(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<string>> ExecuteAsync(CancellationToken stopingToken, string topicName)
        {
            var result = new List<string>();

            var config = new ConsumerConfig
            {
                BootstrapServers = _kafcaConnection,
                GroupId = $"{topicName}-group-0",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            try
            {
                using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
                consumer.Subscribe(topicName);

                try
                {
                    var message = StartProcess;
                    while (!string.IsNullOrEmpty(message))
                    {                        
                        var cr = consumer.Consume(stopingToken);
                        message = cr.Message.Value;
                        result.Add(message);
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

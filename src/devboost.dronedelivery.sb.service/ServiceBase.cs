using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace devboost.dronedelivery.sb.service
{
    public class ServiceBase
    {
        protected readonly string _topicName;
        protected readonly string _kafcaConnection;
        public ServiceBase(string topicName, IConfiguration configuration)
        {
            _topicName = topicName;
            _kafcaConnection = configuration.GetConnectionString("kafkaConnection");
        }
    }
}

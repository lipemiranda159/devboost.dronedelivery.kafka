using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace devboost.dronedelivery.sb.service
{
    public class ServiceBase
    {
        protected readonly string _kafcaConnection;
        public ServiceBase(IConfiguration configuration)
        {
            _kafcaConnection = configuration.GetConnectionString("kafkaConnection");
        }
    }
}

using devboost.dronedelivery.core.domain;
using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.domain.Interfaces.Repositories;
using devboost.dronedelivery.felipe.EF.Repositories;
using devboost.dronedelivery.Infra.Data;
using System;
using System.Collections.Generic;

namespace devboost.dronedelivery.test
{
    public class MockDroneRepository : RepositoryBase<Drone>, IDroneRepository
    {
        public MockDroneRepository(DataContext context) : base(context)
        {
        }

        public List<StatusDroneDto> GetDroneStatus()
        {
            List<StatusDroneDto> statusDroneDtos = new List<StatusDroneDto>();

            var statusDroneDto = new StatusDroneDto
            {
                ClienteId = 1,
                DroneId = 1,
                Latitude = -23.5880684,
                Longitude = -46.6564195,
                Situacao = true,
                PedidoId = 0,
                Nome = string.Empty
            };

            statusDroneDtos.Add(statusDroneDto);

            var statusDroneDto2 = new StatusDroneDto
            {
                ClienteId = 1,
                DroneId = 1,
                Latitude = -23.5880684,
                Longitude = -46.6564195,
                Situacao = false,
                PedidoId = 1,
                Nome = "João da Silva"
            };

            statusDroneDtos.Add(statusDroneDto2);

            return statusDroneDtos;
        }

        public Drone RetornaDrone()
        {
            throw new NotImplementedException();
        }

        public DroneStatusDto RetornaDroneStatus(int droneId)
        {
            throw new NotImplementedException();
        }
    }
}

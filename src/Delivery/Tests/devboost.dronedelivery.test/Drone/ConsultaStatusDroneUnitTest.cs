using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.domain.Entities;
using devboost.dronedelivery.domain.Interfaces.Repositories;
using devboost.dronedelivery.Infra.Data;
using devboost.dronedelivery.Services;
using NSubstitute;
using Xunit;
using devboost.dronedelivery.core.domain.Extensions;
using System;

namespace devboost.dronedelivery.test
{
    public class ConsultaStatusDroneUnitTest
    {
        [Fact]
        public void ConsultarStatusDrone()
        {
            var coordinateService = new CoordinateService();
            IPedidoDroneRepository pedidoDroneRepository = null;
            var context = Substitute.For<DataContext>();
            var droneRepository = new MockDroneRepository(context);
            IPedidoRepository pedidoRepository = null;

            var droneService = new DroneService(coordinateService, pedidoDroneRepository, droneRepository, pedidoRepository);

            var droneStatus = droneService.GetDroneStatus();

            Assert.Equal<int>(2, droneStatus.Count);
        }

        [Fact]
        public void CriarDroneStatusResult()
        {
            var droneStatusResult = new DroneStatusResult
            {
                Id = 1,
                Capacidade = 100,
                Velocidade = 45,
                Autonomia = 30,
                Carga = 650,
                Perfomance = 54.7F,
                SomaPeso = 80,
                SomaDistancia = 8
            };

            Assert.True(droneStatusResult.Id == 1);
            Assert.True(droneStatusResult.Capacidade == 100);
            Assert.True(droneStatusResult.Velocidade == 45);
            Assert.True(droneStatusResult.Autonomia == 30);
            Assert.True(droneStatusResult.Carga == 650);
            Assert.True(droneStatusResult.Perfomance == 54.7F);
            Assert.True(droneStatusResult.SomaPeso == 80);
            Assert.True(droneStatusResult.SomaDistancia == 8);

            Assert.NotNull(droneStatusResult);
        }

        [Fact]
        public void CriarStatusDroneDto()
        {
            var statusDroneDto = new StatusDroneDto
            {
                Situacao = true,
                PedidoId = 5,
                ClienteId = 1,
                Nome = "Cliente Teste",
                Latitude = 765764.98,
                Longitude = 235764.98
            };

            Assert.True(statusDroneDto.Situacao);
            Assert.True(statusDroneDto.PedidoId == 5);
            Assert.True(statusDroneDto.ClienteId == 1);
            Assert.True(statusDroneDto.Nome == "Cliente Teste");
            Assert.True(statusDroneDto.Latitude == 765764.98);
            Assert.True(statusDroneDto.Longitude == 235764.98);


            Assert.NotNull(statusDroneDto);
        }

        [Fact]
        public void TempoGasto()
        {
            devboost.dronedelivery.core.domain.Entities.Drone drone = new devboost.dronedelivery.core.domain.Entities.Drone
            {
                Velocidade = 60
            };

            var result = drone.ToTempoGasto(120);
            
            Assert.Equal(DateTime.Now.AddHours(3).Hour, result.Hour);

        }

    }
}

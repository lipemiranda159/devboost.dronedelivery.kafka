using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.domain.Interfaces.Repositories;
using devboost.dronedelivery.felipe.EF.Repositories;
using devboost.dronedelivery.Infra.Data;
using System.Collections.Generic;
using System.Linq;

namespace devboost.dronedelivery.test
{
    public class ClienteRepositoryFake : RepositoryBase<Cliente>, IClienteRepository
    {

        private readonly List<Cliente> _clientes;

        public ClienteRepositoryFake(DataContext context) : base(context)
        {
            _clientes = new List<Cliente>()
            {
                new Cliente() { Id = 1, Latitude = 23.5741381, Longitude = 46.6617410, Nome = "Marco", Password = "AdminAPIDrone01!" },
                new Cliente() { Id = 2, Latitude = 23.5746581, Longitude = 46.6618522, Nome = "Felipe", Password = "AdminAPIDrone02!" },
                new Cliente() { Id = 3, Latitude = 23.5741247, Longitude = 46.6619637, Nome = "Lucas", Password = "AdminAPIDrone03!" },
                new Cliente() { Id = 4, Latitude = 23.5748520, Longitude = 46.6617894, Nome = "Italo", Password = "AdminAPIDrone04!" },
                new Cliente() { Id = 5, Latitude = 23.5743698, Longitude = 46.6614561, Nome = "Danilo", Password = "AdminAPIDrone05!" },

            };
        }

        public IEnumerable<Cliente> GetClientes()
        {
            return _clientes;
        }

        public int GerarId()
        {
            var id = _clientes.Max(_ => _.Id) + 1;
            return id;
        }

    }
}

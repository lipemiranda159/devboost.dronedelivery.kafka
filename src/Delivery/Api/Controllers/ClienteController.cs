using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devboost.dronedelivery.Api.Controllers
{
    /// <summary>
    /// Controller com ações referentes aos clientes
    /// </summary>
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController
    {
        private readonly IClienteRepository _clienteRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="clienteRepository"></param>
        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        /// <summary>
        /// Retorna lista de clientes cadastrados no banco
        /// </summary>
        /// <returns>Uma lista de clientes</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Cliente>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            return clientes.ToList();
        }
        /// <summary>
        /// Cria um novo cliente
        /// </summary>
        /// <param name="cliente">Modelo cliente</param>
        /// <remarks>
        ///Exemplo:
        ///
        ///     POST /api/cliente
        ///     {
        ///      "nome": "Joao",
        ///      "latitude": -23.5631043,
        ///      "longitude": -46.6565712,
        ///      "userId": "joao",
        ///      "password": "1234"
        ///     }
        ///
        /// </remarks>        
        /// <returns>O novo cliente</returns>
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<Cliente>> Post(Cliente cliente)
        {
            await _clienteRepository.AddAsync(cliente);
            return cliente;
        }

    }
}

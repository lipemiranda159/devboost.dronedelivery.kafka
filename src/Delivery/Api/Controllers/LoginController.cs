using devboost.dronedelivery.security.domain.Entites;
using devboost.dronedelivery.security.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace devboost.dronedelivery.Api.Controllers
{

    /// <summary>
    /// Controller com operações referentes aos drones
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController
    {
        private readonly AccessManager _accessManager;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accessManager"></param>
        public LoginController(AccessManager accessManager)
        {
            _accessManager = accessManager;
        }
        /// <summary>
        /// Login do usuário
        /// </summary>
        /// <param name="usuario"></param>
        ///<remarks>
        ///Exemplo:
        ///
        ///     POST /api/login
        ///     {
        ///         "userId": "teste",
        ///         "password": "teste",
        ///     }
        /// </remarks>
        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Post([FromBody] LoginDTO usuario)
        {
            if (await _accessManager.ValidateCredentials(usuario))
            {
                return _accessManager.GenerateToken(usuario);
            }
            return new
            {
                Authenticated = false,
                Message = "Falha ao autenticar"
            };
        }


    }
}

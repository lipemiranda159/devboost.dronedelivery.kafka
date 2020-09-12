using devboost.dronedelivery.core.domain.Entities;
using devboost.dronedelivery.domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace devboost.dronedelivery.Api.Controllers
{
    /// <summary>
    /// Controller com ações referentes aos drones
    /// </summary>
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class DronesController : ControllerBase
    {
        private readonly IDroneFacade _droneFacade;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="droneFacade"></param>
        public DronesController(IDroneFacade droneFacade)
        {
            _droneFacade = droneFacade;
        }
        /// <summary>
        /// Retorna status dos drones
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetStatusDrone")]
        [AllowAnonymous]
        public ActionResult<List<StatusDroneDto>> GetStatusDrone()
        {
            return Ok(_droneFacade.GetDroneStatus());
        }
        /// <summary>
        /// Adiciona um novo drone
        /// </summary>
        /// <param name="drone"></param>
        ///<remarks>
        ///Exemplo:
        ///
        ///     POST /api/drone
        ///     {
        ///         "capacidade": 100,
        ///         "velocidade": 100,
        ///         "autonomia": 100,
        ///         "carga": 100,
        ///     }
        /// </remarks>
        /// <returns>O novo drone</returns>
        [ProducesResponseType(typeof(Drone), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<Drone>> PostDrone(Drone drone)
        {
            return await _droneFacade.SaveDroneAsync(drone);
        }
    }
}

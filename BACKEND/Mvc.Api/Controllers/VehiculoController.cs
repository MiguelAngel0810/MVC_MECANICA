using DtoModel.Vehiculo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mvc.Bussnies.Vehiculo;

namespace Mvc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class VehiculoController : ControllerBase
    {
        private readonly IVehiculoBussnies _vehiculoBussnies;

        public VehiculoController(IVehiculoBussnies vehiculoBussnies)
        {
            _vehiculoBussnies = vehiculoBussnies;
        }

        [HttpGet]
        public async Task<ActionResult<List<VehiculoDto>>> GetAll()
        {
            List<VehiculoDto> list = await _vehiculoBussnies.GetAll();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehiculoDto>> GetById(int id)
        {
            VehiculoDto? vehiculo = await _vehiculoBussnies.GetById(id);

            if (vehiculo == null)
            {
                return NotFound(new { message = "Vehículo no encontrado" });
            }

            return Ok(vehiculo);
        }

        [HttpPost]
        public async Task<ActionResult<VehiculoDto>> Create([FromBody] VehiculoDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VehiculoDto vehiculo = await _vehiculoBussnies.Create(request);

            return CreatedAtAction(nameof(GetById), new { id = vehiculo.VehiculoId }, vehiculo);
        }

        [HttpPut]
        public async Task<ActionResult<VehiculoDto>> Update([FromBody] VehiculoDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VehiculoDto? vehiculo = await _vehiculoBussnies.Update(request);

            if (vehiculo == null)
            {
                return NotFound(new { message = "Vehículo no encontrado" });
            }

            return Ok(vehiculo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            VehiculoDto? vehiculo = await _vehiculoBussnies.GetById(id);

            if (vehiculo == null)
            {
                return NotFound(new { message = "Vehículo no encontrado" });
            }

            await _vehiculoBussnies.Delete(id);

            return NoContent();
        }
    }
}

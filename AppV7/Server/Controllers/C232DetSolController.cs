using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C232DetSolController : ControllerBase
    {
        private readonly I232DetSol _detSolIFace;

        public C232DetSolController(I232DetSol detSolIFace)
        {
            _detSolIFace = detSolIFace;
        }
        [HttpGet("{filtro}")]
        public async Task<ActionResult<IEnumerable<Z232_DetSol>>> Buscar(string clave)
        {
            try
            {
                var resultado = await _detSolIFace.Buscar(clave);
                return Ok(resultado);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al leer la base de datos, buscando registros de detalles de la solicitud");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Z232_DetSol>> AddDetSol(Z232_DetSol detSol)
        {
            try
            {
                if (detSol == null) return BadRequest();
                return await _detSolIFace.AddDetalle(detSol);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar crear un nuevo detalle de solicitud en base de datos.");
            }
        }
        [HttpPut]
        public async Task<ActionResult<Z232_DetSol>> UpdateOrg(Z232_DetSol detSol)
        {
            try
            {
                return detSol != null ? await _detSolIFace.UpDateDetalle(detSol) :
                    NotFound($"El detalle no fue encontrado");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar actualizar la base de datos de los detalles de solicitud");
            }
        }
    }
}

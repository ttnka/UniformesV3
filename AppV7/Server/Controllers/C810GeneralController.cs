using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C810GeneralController : ControllerBase
    {
        private readonly I810General _gralIFace;

        public C810GeneralController(I810General gralIFase)
        {
            _gralIFace = gralIFase;
        }
        [HttpGet("{filtro}")]
        public async Task<ActionResult<IEnumerable<Z810_General>>> Buscar(string clave)
        {
            try
            {
                var resultado = await _gralIFace.Buscar(clave);
                return Ok(resultado);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al leer la base de datos, buscando registros de paginas General");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Z810_General>> AddGeneral(Z810_General gral)
        {
            try
            {
                if (gral == null) return BadRequest();
                return await _gralIFace.AddGeneral(gral);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar crear un nuevo registro paginas general en base de datos.");
            }
        }
        [HttpPut]
        public async Task<ActionResult<Z810_General>> UpdateGeneral(Z810_General gral)
        {
            try
            {
                return gral != null ? await _gralIFace.UpDateGeneral(gral) :
                    NotFound($"El registro pagina general no fue encontrado");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar actualizar la base de datos de las paginas general");
            }
        }
    }
}

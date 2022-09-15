using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C204ContDetController : ControllerBase
    {
        private readonly I204ContDet _contDetIFace;

        public C204ContDetController(I204ContDet contDetIFace)
        {
            _contDetIFace = contDetIFace;
        }

        [HttpGet("{filtro}")]
        public async Task<ActionResult<IEnumerable<Z204_ContDet>>> Buscar(string clave)
        {
            try
            {
                var resultado = await _contDetIFace.Buscar(clave);
                return Ok(resultado);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al leer la base de datos, buscando registros de Datos de contactos.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Z204_ContDet>> AddContDet(Z204_ContDet contDet)
        {
            try
            {
                if (contDet == null) return BadRequest();
                return await _contDetIFace.AddContDet(contDet);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar crear un nuevo registro de Datos de Contactos la base de datos.");
            }
        }
        [HttpPut]
        public async Task<ActionResult<Z204_ContDet>> UpDateContDet(Z204_ContDet conctDet)
        {
            try
            {
                return conctDet != null ? await _contDetIFace.UpdateContDet(conctDet) :
                    NotFound($"El Usuario no fue encontrado");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar actualizar la base de datos, de los Datos de contactos en las organizaciones");
            }
        }
    }
}

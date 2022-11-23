using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C210AlmacenController : ControllerBase
    {
        private readonly I210Almacen _almacenIFace;

        public C210AlmacenController(I210Almacen almacenIFace)
        {
            _almacenIFace = almacenIFace;
        }
        [HttpGet("{filtro}")]
        public async Task<ActionResult<IEnumerable<Z210_Almacen>>> Buscar(string clave)
        {
            try
            {
                var resultado = await _almacenIFace.Buscar(clave);
                return Ok(resultado);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al leer la base de datos, buscando registros de Almacenes");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Z210_Almacen>> AddAlmacen(Z210_Almacen almacen)
        {
            try
            {
                if (almacen == null) return BadRequest();
                return await _almacenIFace.AddAlmacen(almacen);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar crear un nuevo Almacen en base de datos.");
            }
        }
        [HttpPut]
        public async Task<ActionResult<Z210_Almacen>> UpdateOrg(Z210_Almacen almacen)
        {
            try
            {
                return almacen != null ? await _almacenIFace.UpDateAlmacen(almacen) :
                    NotFound($"El almancen no fue encontrado");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar actualizar la base de datos de los almacenes");
            }
        }
    }
}

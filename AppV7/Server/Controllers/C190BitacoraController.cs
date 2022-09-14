using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C190BitacoraController : ControllerBase
    {
        private readonly I190Bitacora _bitacoraIFace;

        public C190BitacoraController(I190Bitacora bitacoraIFace)
        {
            this._bitacoraIFace = bitacoraIFace;
        }

        [HttpGet("{filtro}")]
        public async Task<ActionResult<IEnumerable<Z190_Bitacora>>> Buscar(string clave,
            string orgX)
        {
            try
            {
                var resultado = await _bitacoraIFace.Buscar(clave, orgX);
                return Ok(resultado);
                //return resultado.Any() ? Ok(resultado) : NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al leer la base de datos, buscando registros de bitacora");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Z190_Bitacora>> AddBitacora(Z190_Bitacora bitacora)
        {
            try
            {
                if (bitacora == null) return BadRequest();
                return await _bitacoraIFace.AddBitacora(bitacora);
                /*
                var newBitacora = await _bitacoraIFace.AddBitacora(bitacora);
                return CreatedAtAction(nameof(GetBitacora), new { bitacoraId = bitacora.Id }, newBitacora);
                */
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar crear un nuevo registro de bitacora en la base de datos2.");
            }
        }
    }
}

using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C840ContactanosController : ControllerBase
    {
        private readonly I840Contactanos _contactanosIFace;

        public C840ContactanosController(I840Contactanos contactanosIFace)
        {
            _contactanosIFace = contactanosIFace;
        }
        [HttpGet("{filtro}")]
        public async Task<ActionResult<IEnumerable<Z840_Contactanos>>> Buscar(string clave)
        {
            try
            {
                var resultado = await _contactanosIFace.Buscar(clave);
                return Ok(resultado);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al leer la base de datos, buscando registros de contactanos website.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Z840_Contactanos>> AddContactanos(Z840_Contactanos contactanos)
        {
            try
            {
                if (contactanos == null) return BadRequest();
                return await _contactanosIFace.AddContactanos(contactanos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar crear un nuevo registro de Contactanos website la base de datos.");
            }
        }
        [HttpPut]
        public async Task<ActionResult<Z840_Contactanos>> UpdateContactanos(Z840_Contactanos contactanos)
        {
            try
            {
                return contactanos != null ? await _contactanosIFace.UpdateContactanos(contactanos) :
                    NotFound($"El registro de contactanos website no fue encontrado");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar actualizar la base de datos, de los registros de contactanos website");
            }
        }
    }
}

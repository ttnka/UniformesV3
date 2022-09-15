using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C202ContactoController : ControllerBase 
    {
        private readonly I202Contacto _contactoIFace;

        public C202ContactoController(I202Contacto contactoIFace)
        {
            _contactoIFace = contactoIFace;
        }

        [HttpGet("{filtro}")]
        public async Task<ActionResult<IEnumerable<Z202_Contacto>>> Buscar(string clave)
        {
            try
            {
                var resultado = await _contactoIFace.Buscar(clave);
                return Ok(resultado);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al leer la base de datos, buscando registros de contactos de la organizacion");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Z202_Contacto>> AddContacto(Z202_Contacto contacto)
        {
            try
            {
                if (contacto == null) return BadRequest();
                return await _contactoIFace.AddContacto(contacto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar crear un nuevo registro de Contactos la base de datos.");
            }
        }
        [HttpPut]
        public async Task<ActionResult<Z202_Contacto>> UpDateUsuario(Z202_Contacto contacto)
        {
            try
            {
                return contacto != null ? await _contactoIFace.UpdateContacto(contacto) :
                    NotFound($"El Usuario no fue encontrado");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar actualizar la base de datos, de los contactos en las organizaciones");
            }
        }
    }
}

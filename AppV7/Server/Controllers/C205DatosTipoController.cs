using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C205DatosTipoController : ControllerBase
    {
        private readonly I205DatosTipo _datosTipoIFace;

        public C205DatosTipoController(I205DatosTipo datosTipoIFace)
        {
            _datosTipoIFace = datosTipoIFace;
        }

        [HttpGet("{filtro}")]
        public async Task<ActionResult<IEnumerable<Z205_DatosTipo>>> Buscar(string clave)
        {
            try
            {
                var resultado = await _datosTipoIFace.Buscar(clave);
                return Ok(resultado);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al leer la base de datos, buscando registros de tipos de datos para contactos.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Z205_DatosTipo>> AddDatosTipo(Z205_DatosTipo datosTipo)
        {
            try
            {
                if (datosTipo == null) return BadRequest();
                return await _datosTipoIFace.AddDatosTipo(datosTipo);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar crear un nuevo registro de datos tipo para Contactos la base de datos.");
            }
        }
        [HttpPut]
        public async Task<ActionResult<Z205_DatosTipo>> UpdateDatosTipo(Z205_DatosTipo datosTipo)
        {
            try
            {
                return datosTipo != null ? await _datosTipoIFace.UpdateDatosTipo(datosTipo) :
                    NotFound($"El registro del tipo de datos para contactos no fue encontrado");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar actualizar la base de datos, de datos tipos de los contactos en las organizaciones");
            }
        }
    }
}

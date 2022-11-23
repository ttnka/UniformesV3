using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C230SolicitudController : ControllerBase
    {
        private readonly I230Solicitud _solIFace;

        public C230SolicitudController(I230Solicitud solIFace)
        {
            _solIFace = solIFace;
        }
        [HttpGet("{filtro}")]
        public async Task<ActionResult<IEnumerable<Z230_Solicitud>>> Buscar(string clave)
        {
            try
            {
                var resultado = await _solIFace.Buscar(clave);
                return Ok(resultado);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al leer la base de datos, buscando registros de solicitudes");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Z230_Solicitud>> AddSolicitud(Z230_Solicitud solicitud)
        {
            try
            {
                if (solicitud == null) return BadRequest();
                return await _solIFace.AddSolicitud(solicitud);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar crear una nueva solicitud en base de datos.");
            }
        }
        [HttpPut]
        public async Task<ActionResult<Z230_Solicitud>> UpdateOrg(Z230_Solicitud solicitud)
        {
            try
            {
                return solicitud != null ? await _solIFace.UpDateSolicitud(solicitud) :
                    NotFound($"La solicitud no fue encontrado");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar actualizar la base de datos de las solicitudes");
            }
        }
    }
}

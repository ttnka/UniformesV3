using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C260FolioController : ControllerBase
    {
        private readonly I260Folio _folioIFace;

        public C260FolioController(I260Folio folioIFace)
        {
            _folioIFace = folioIFace;
        }

        [HttpGet("{filtro}")]
        public async Task<ActionResult<IEnumerable<Z260_Folio>>> Buscar(string clave)
        {
            try
            {
                var resultado = await _folioIFace.Buscar(clave);
                return Ok(resultado);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al leer la base de datos buscando folios agregados de los comerciantes");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Z260_Folio>> AddFolio(Z260_Folio folio)
        {
            try
            {
                if (folio == null) return BadRequest();
                return await _folioIFace.AddFolio(folio);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar un nuevo folio de los comerciantes.");
            }
        }
        [HttpPut]
        public async Task<ActionResult<Z260_Folio>> UpDateFolio(Z260_Folio folio)
        {
            try
            {
                return folio != null ? await _folioIFace.UpDateFolio(folio) :
                    NotFound($"El folio no fue encontrado");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar actualizar la base de datos de folios de comerciantes.");
            }
        }
    }
}

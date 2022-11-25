using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C290MetaController : ControllerBase
    {
        private readonly I290Meta _metaIFace;
        public C290MetaController(I290Meta metaIFace)
        {
            _metaIFace = metaIFace;
        }
        [HttpGet("{filtro}")]
        public async Task<ActionResult<IEnumerable<Z290_Meta>>> Buscar(string clave)
        {
            try
            {
                var resultado = await _metaIFace.Buscar(clave);
                return Ok(resultado);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al leer la base de datos buscando metas.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Z290_Meta>> AddMeta(Z290_Meta meta)
        {
            try
            {
                if (meta == null) return BadRequest();
                return await _metaIFace.AddMeta(meta);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar crear una nueva meta en la base de datos.");
            }
        }
        [HttpPut]
        public async Task<ActionResult<Z290_Meta>> UpDateUsuario(Z290_Meta meta)
        {
            try
            {
                return meta != null ? await _metaIFace.UpDateMeta(meta) :
                    NotFound($"El meta no fue encontrada");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar actualizar la base de datos de las metas");
            }
        }
    }
}

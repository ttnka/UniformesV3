using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C800WebSiteController : ControllerBase 
    {
        private readonly I800WebSite _webSiteIFace;

        public C800WebSiteController(I800WebSite webSiteIFace)
        {
            _webSiteIFace = webSiteIFace;
        }

        [HttpGet("{filtro}")]
        public async Task<ActionResult<IEnumerable<Z800_WebSite>>> Buscar(string clave)
        {
            try
            {
                var resultado = await _webSiteIFace.Buscar(clave);
                return Ok(resultado);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al leer la base de datos, buscando registros de modulos del WebSite");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Z800_WebSite>> AddWebSite(Z800_WebSite webSite)
        {
            try
            {
                if (webSite == null) return BadRequest();
                return await _webSiteIFace.AddWebSite(webSite);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar crear un nuevo registro de webSites la base de datos.");
            }
        }
        [HttpPut]
        public async Task<ActionResult<Z800_WebSite>> UpdateWebSite(Z800_WebSite webSite)
        {
            try
            {
                return webSite != null ? await _webSiteIFace.UpdateWebSite(webSite) :
                    NotFound($"El modulo del WebSite no fue encontrado");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar actualizar la base de datos, de los Modulos del WebSites");
            }
        }
    }
}

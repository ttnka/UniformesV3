using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C100OrgController : ControllerBase 
    {
        private readonly I100Org _orgIFace;

        public C100OrgController(I100Org orgIFace)
        {
            _orgIFace = orgIFace;
        }

        [HttpGet("{filtro}")]
        public async Task<ActionResult<IEnumerable<Z100_Org>>> Buscar(string clave)
        {
            try
            {
                var resultado = await _orgIFace.Buscar(clave);
                return Ok(resultado);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al leer la base de datos, buscando registros de organizaciones");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Z100_Org>> AddOrg(Z100_Org org)
        {
            try
            {
                if (org == null) return BadRequest();
                return await _orgIFace.AddOrg(org);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar crear un nueva Organizacion en base de datos2.");
            }
        }
        [HttpPut]
        public async Task<ActionResult<Z100_Org>> UpdateOrg(Z100_Org org)
        {
            try
            {
                return org != null ? await _orgIFace.UpDateOrg(org) :
                    NotFound($"La Organizacion no fue encontrado");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar actualizar la base de datos, de las organizaciones");
            }
        }
    }
}

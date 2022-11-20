using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C110UsuariosController : ControllerBase 
    {
        private readonly I110Usuarios _userIFace;

        public C110UsuariosController(I110Usuarios userIFace)
        {
            _userIFace = userIFace;
        }

        [HttpGet("{filtro}")]
        public async Task<ActionResult<IEnumerable<Z110_Usuarios>>> Buscar(string clave,
            string orgX)
        {
            try
            {
                var resultado = await _userIFace.Buscar(clave, orgX);
                
                return Ok(resultado);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al leer la base de datos, buscando registros de usurios");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Z110_Usuarios>> AddUsuario(Z110_Usuarios usuario)
        {
            try
            {
                if (usuario == null) return BadRequest();
                return await _userIFace.AddUsuario(usuario);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar crear un nuevo registro de USUARIO la base de datos2.");
            }
        }
        [HttpPut]
        public async Task<ActionResult<Z110_Usuarios>> UpDateUsuario(Z110_Usuarios usuario)
        {
            try
            {
                return usuario != null ? await _userIFace.UpDateUsuario(usuario) :
                    NotFound($"El Usuario no fue encontrado");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar actualizar la base de datos, de las organizaciones");
            }
        }
    }
}

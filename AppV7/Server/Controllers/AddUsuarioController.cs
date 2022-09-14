using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddUsuarioController : ControllerBase 
    {
        private readonly IAddUsuario _addUsuarioIFace;

        public AddUsuarioController(IAddUsuario addUsuarioIFace)
        {
            _addUsuarioIFace = addUsuarioIFace;
        }

        [HttpPost]
        public async Task<ActionResult<EAddUsuario>> AddUsuario(EAddUsuario newAU)
        {
            try
            {
               var resultado = await _addUsuarioIFace.AddUsuario(newAU);
               return resultado;   //return await _addUsuarioIFace.AddUsuario(newAU);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar crear un nuevo USUARIO en el sistema.");
            }
        }

        [HttpPost("{recupera}")]
        public async Task<ActionResult<ERecupera>> Recupera(ERecupera recupera)
        {
            try
            {
                var resultado = await _addUsuarioIFace.Recupera(recupera);
                return resultado;   //return await _addUsuarioIFace.AddUsuario(newAU);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar crear un nuevo USUARIO en el sistema.");
            }
        }

        [HttpPost("{reset}")]
        public async Task<ActionResult<EAddUsuario>> ResetPass(EAddUsuario eAddUser)
        {
            try
            {
                var resultado = await _addUsuarioIFace.ResetPassword(eAddUser);
                return resultado;   //return await _addUsuarioIFace.AddUsuario(newAU);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar reiniciar tu password en el sistema.");
            }
        }
    }
}

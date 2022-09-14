using AppV7.Server.Models.IFace;
using Microsoft.AspNetCore.Mvc;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRegIniController : ControllerBase
    {
        private readonly IRegistroInicial _rgIServ;

        public CRegIniController(IRegistroInicial rgIServ)
        {
            _rgIServ = rgIServ;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> RegInicial(bool dato)
        {
            try
            {
                return await _rgIServ.DoRegInicial();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar crear el primer registro en base de datos.");
            }
        }
    }
}

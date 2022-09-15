using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C195MailUsController : ControllerBase
    {
        private readonly I195MailUs _mailUsIFace;

        public C195MailUsController(I195MailUs mailUsIFace)
        {
            _mailUsIFace = mailUsIFace;
        }

        [HttpGet("{filtro}")]
        public async Task<ActionResult<IEnumerable<Z195_MailUs>>> Buscar(string clave)
        {
            try
            {
                var resultado = await _mailUsIFace.Buscar(clave);
                return Ok(resultado);
                //return resultado.Any() ? Ok(resultado) : NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al leer la base de datos, buscando registros de Mails enviados a nuestro contacto");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Z195_MailUs>> AddMailUs(Z195_MailUs mailUs)
        {
            try
            {
                if (mailUs == null) return BadRequest();
                return await _mailUsIFace.AddMailUs(mailUs);
                /*
                var newmailUs = await _mailUsIFace.AddmailUs(mailUs);
                return CreatedAtAction(nameof(GetmailUs), new { mailUsId = mailUs.Id }, newmailUs);
                */
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar crear un nuevo registro de mailUs en la base de datos2.");
            }
        }
    }
}

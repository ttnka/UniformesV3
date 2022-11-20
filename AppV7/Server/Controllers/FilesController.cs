using AppV7.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public FilesController(IWebHostEnvironment env)
        {
            _env = env;
        }
        [HttpPost]
        public async Task<ActionResult<List<Z812_Upload>>>
            UpLoad([FromForm] List<IFormFile> files)
        {
            try
            {
                List<Z812_Upload> upLoadResults = new List<Z812_Upload>();
                foreach (var file in files)
                {
                    var uploadresult = new Z812_Upload();
                    uploadresult.FileName = file.FileName;
                    string folder = @"Imagenes\News";
                    var path = Path.Combine(_env.ContentRootPath, folder, file.FileName) ;
                    await using FileStream fs = new(path, FileMode.Create);

                    await file.CopyToAsync(fs);
                    uploadresult.StoredName = file.FileName;
                    upLoadResults.Add(uploadresult);
                }
                return Ok(upLoadResults);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error al subir un archivo");
            }
        }
     }
}

using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using System.Globalization;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly I260Folio _folio;

        public FilesController(IWebHostEnvironment env, I260Folio folio)
        {
            _env = env;
            _folio = folio;
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
    #region Subir archivo
                    var uploadresult = new Z812_Upload();
                    uploadresult.FileName = file.FileName;
                    string folder = @"Imagenes\Folios";
                    var path = Path.Combine(_env.ContentRootPath, folder, file.FileName) ;
                    await using FileStream fs = new(path, FileMode.Create);

                    await file.CopyToAsync(fs);
                    uploadresult.StoredName = file.FileName;
                    upLoadResults.Add(uploadresult);
    #endregion
    
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

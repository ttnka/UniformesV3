using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C812FilesController : ControllerBase
    {
        private readonly I812Files _filesIFace;
        private readonly IWebHostEnvironment _env;

        public C812FilesController(I812Files filesIFace, IWebHostEnvironment env)
        {
            _filesIFace = filesIFace;
            _env = env;
        }
        [HttpPost]
        public async Task<ActionResult<List<Z812_Upload>>> Upload(List<IFormFile> files)
        {
            try
            {
                List<Z812_Upload> upLoadResults = new List<Z812_Upload>();
                foreach(var file in files)
                {
                    var uploadresult = new Z812_Upload();
                    uploadresult.FileName = file.FileName;
                    string folder = @"Imagenes\News";
                    var path = Path.Combine(_env.ContentRootPath, folder, file.FileName);
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
                                    "Error al leer subir un archivo");
            }
        }

        [HttpGet("{filtro}")]
        public async Task<ActionResult<IEnumerable<Z812_Files>>> Buscar(string clave)
        {
            try
            {
                var resultado = await _filesIFace.Buscar(clave);
                return Ok(resultado);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al leer la base de datos, buscando archivos o fotos");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Z812_Files>> AddOrg(Z812_Files files)
        {
            try
            {
                if (files == null) return BadRequest();
                return await _filesIFace.AddFile(files);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar agregar un archivo en base de datos.");
            }
        }
        [HttpPut]
        public async Task<ActionResult<Z812_Files>> UpdateFile(Z812_Files files)
        {
            try
            {
                return files != null ? await _filesIFace.UpDateFile(files) :
                    NotFound($"Los archivos o fotos no fueron encontrados");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar actualizar la base de datos con archivos o fotos.");
            }
        }
    }
}

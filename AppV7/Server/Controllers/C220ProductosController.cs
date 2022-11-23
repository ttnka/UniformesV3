using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AppV7.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C220ProductosController : ControllerBase
    {
        private readonly I220Producto _productoIFace;

        public C220ProductosController(I220Producto productoIFace)
        {
            _productoIFace = productoIFace;
        }
        [HttpGet("{filtro}")]
        public async Task<ActionResult<IEnumerable<Z220_Producto>>> Buscar(string clave)
        {
            try
            {
                var resultado = await _productoIFace.Buscar(clave);
                return Ok(resultado);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al leer la base de datos, buscando registros de productos");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Z220_Producto>> AddProducto(Z220_Producto producto)
        {
            try
            {
                if (producto == null) return BadRequest();
                return await _productoIFace.AddProducto(producto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar crear un nuevo producto en base de datos.");
            }
        }
        [HttpPut]
        public async Task<ActionResult<Z220_Producto>> UpdateProducto(Z220_Producto producto)
        {
            try
            {
                return producto != null ? await _productoIFace.UpDateProducto(producto) :
                    NotFound($"El producto no fue encontrado");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error al intentar actualizar la base de datos en los productos.");
            }
        }
    }
}

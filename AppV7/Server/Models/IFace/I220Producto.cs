using AppV7.Shared;

namespace AppV7.Server.Models.IFace
{
    public interface I220Producto
    {
        Task<IEnumerable<Z220_Producto>> Buscar(string clave);
        Task<Z220_Producto> AddProducto(Z220_Producto producto);
        Task<Z220_Producto> UpDateProducto(Z220_Producto producto);
    }
}

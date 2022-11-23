using AppV7.Shared;

namespace AppV7.Client.Servicios.IFaceServ
{
    public interface I220ProductoServ
    {
        Task<IEnumerable<Z220_Producto>> Buscar(string clave);
        Task<Z220_Producto> AddProducto(Z220_Producto producto);
        Task<Z220_Producto> UpDateProducto(Z220_Producto producto);
    }
}

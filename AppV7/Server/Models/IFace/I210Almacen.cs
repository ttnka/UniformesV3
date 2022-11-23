using AppV7.Shared;

namespace AppV7.Server.Models.IFace
{
    public interface I210Almacen
    {
        Task<IEnumerable<Z210_Almacen>> Buscar(string clave);
        Task<Z210_Almacen> AddAlmacen(Z210_Almacen almacen);
        Task<Z210_Almacen> UpDateAlmacen(Z210_Almacen almacen);
    }
}

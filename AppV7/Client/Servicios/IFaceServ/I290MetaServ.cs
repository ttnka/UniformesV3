using AppV7.Shared;

namespace AppV7.Client.Servicios.IFaceServ
{
    public interface I290MetaServ
    {
        Task<IEnumerable<Z290_Meta>> Buscar(string clave);
        Task<Z290_Meta> AddMeta(Z290_Meta meta);
        Task<Z290_Meta> UpDateMeta(Z290_Meta meta);
    }
}

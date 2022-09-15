using AppV7.Shared;

namespace AppV7.Server.Models.IFace
{
    public interface I800WebSite
    {
        Task<IEnumerable<Z800_WebSite>> Buscar(string clave);

        Task<Z800_WebSite> AddWebSite(Z800_WebSite webSite);
        Task<Z800_WebSite> UpdateWebSite(Z800_WebSite webSite);
    }
}

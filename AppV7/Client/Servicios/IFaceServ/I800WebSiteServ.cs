using AppV7.Shared;

namespace AppV7.Client.Servicios.IFaceServ
{
    public interface I800WebSiteServ
    {
        Task<IEnumerable<Z800_WebSite>> Buscar(string clave);
        Task<Z800_WebSite> AddWebSite(Z800_WebSite webSite);
        Task<Z800_WebSite> UpdateWebSite(Z800_WebSite webSite);
    }
}

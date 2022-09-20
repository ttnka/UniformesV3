using AppV7.Shared;

namespace AppV7.Client.Servicios.IFaceServ
{
    public interface I802WebConfigServ
    {
        Task<IEnumerable<Z802_WebConfig>> Buscar(string clave);
        Task<Z802_WebConfig> AddWebConfig(Z802_WebConfig wConfig);
        Task<Z802_WebConfig> UpDateWebConfig(Z802_WebConfig wConfig);
    }
}

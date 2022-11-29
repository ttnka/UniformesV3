using AppV7.Shared;

namespace AppV7.Server.Models.IFace
{
    public interface I230Solicitud
    {
        Task<IEnumerable<Z230_Solicitud>> Buscar(string clave);
        Task<Z230_Solicitud> AddSolicitud(Z230_Solicitud solicitud);
        Task<Z230_Solicitud> UpDateSolicitud(Z230_Solicitud solicitud);
        
    }
}

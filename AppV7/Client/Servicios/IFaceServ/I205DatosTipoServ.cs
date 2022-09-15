using AppV7.Shared;

namespace AppV7.Client.Servicios.IFaceServ
{
    public interface I205DatosTipoServ
    {
        Task<IEnumerable<Z205_DatosTipo>> Buscar(string clave);
        Task<Z205_DatosTipo> AddDatosTipo(Z205_DatosTipo datosTipo);
        Task<Z205_DatosTipo> UpdateDatosTipo(Z205_DatosTipo datosTipo);
    }
}

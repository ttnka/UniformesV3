using AppV7.Shared;
namespace AppV7.Server.Models.IFace
{
    public interface I205DatosTipo
    {
        Task<IEnumerable<Z205_DatosTipo>> Buscar(string clave);
        Task<Z205_DatosTipo> AddDatosTipo(Z205_DatosTipo datosTipo);
        Task<Z205_DatosTipo> UpdateDatosTipo(Z205_DatosTipo datosTipo);
    }
}

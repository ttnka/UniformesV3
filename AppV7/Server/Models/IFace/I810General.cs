using AppV7.Shared;

namespace AppV7.Server.Models.IFace
{
    public interface I810General
    {
        Task<IEnumerable<Z810_General>> Buscar(string clave);
        Task<Z810_General> AddGeneral(Z810_General gral);
        Task<Z810_General> UpDateGeneral(Z810_General gral);
    }
}

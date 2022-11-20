using AppV7.Shared;

namespace AppV7.Server.Models.IFace
{
    public interface I812Files
    {
        Task<IEnumerable<Z812_Files>> Buscar(string clave);
        Task<Z812_Files> AddFile(Z812_Files files);
        Task<Z812_Files> UpDateFile(Z812_Files files);
    }
}


using AppV7.Shared;
using Microsoft.AspNetCore.Http;

namespace AppV7.Client.Servicios.IFaceServ
{
    public interface I812FilesServ
    {
        Task<IEnumerable<Z812_Upload>> UploadFiles(MultipartFormDataContent filesList);
        Task<IEnumerable<Z812_Files>> Buscar(string clave);
        Task<Z812_Files> AddFile(Z812_Files files);
        Task<Z812_Files> UpDateFile(Z812_Files files);
    }
}

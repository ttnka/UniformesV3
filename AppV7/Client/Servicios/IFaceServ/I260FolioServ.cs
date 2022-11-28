using AppV7.Shared;

namespace AppV7.Client.Servicios.IFaceServ
{
    public interface I260FolioServ 
    {
        Task<IEnumerable<Z260_Folio>> Buscar(string clave);
        Task<Z260_Folio> AddFolio(Z260_Folio folio);
        Task<Z260_Folio> UpDateFolio(Z260_Folio folio);
        Task<bool> AddFoliosVarios(List<Z260_Folio> folio);
    }
}

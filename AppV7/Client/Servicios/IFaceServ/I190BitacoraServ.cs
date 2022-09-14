using AppV7.Shared;

namespace AppV7.Client.Servicios.IFaceServ
{
    public interface I190BitacoraServ
    {
        Task<IEnumerable<Z190_Bitacora>> Buscar(string clave, string orgX);

        Task<Z190_Bitacora> AddBitacora(Z190_Bitacora bitacora);
    }
}

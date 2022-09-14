using AppV7.Shared;

namespace AppV7.Server.Models.IFace
{
    public interface I190Bitacora
    {
        Task<IEnumerable<Z190_Bitacora>> Buscar(string clave, string orgX);

        Task<Z190_Bitacora> AddBitacora(Z190_Bitacora bitacora);
    }
}


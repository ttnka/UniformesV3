using AppV7.Shared;

namespace AppV7.Server.Models.IFace
{
    public interface I840Contactanos
    {
        Task<IEnumerable<Z840_Contactanos>> Buscar(string clave);

        Task<Z840_Contactanos> AddContactanos(Z840_Contactanos contactanos);
        Task<Z840_Contactanos> UpdateContactanos(Z840_Contactanos contactanos);
    }
}

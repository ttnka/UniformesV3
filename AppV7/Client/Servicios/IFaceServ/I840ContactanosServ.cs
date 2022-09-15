using AppV7.Shared;

namespace AppV7.Client.Servicios.IFaceServ
{
    public interface I840ContactanosServ
    {
        Task<IEnumerable<Z840_Contactanos>> Buscar(string clave);
        Task<Z840_Contactanos> AddContactanos(Z840_Contactanos contactanos);
        Task<Z840_Contactanos> UpdateContactanos(Z840_Contactanos contactanos);
    }
}

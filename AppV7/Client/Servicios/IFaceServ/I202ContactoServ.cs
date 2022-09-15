using AppV7.Shared;

namespace AppV7.Client.Servicios.IFaceServ
{
    public interface I202ContactoServ
    {
        Task<IEnumerable<Z202_Contacto>> Buscar(string clave);
        Task<Z202_Contacto> AddContacto(Z202_Contacto contacto);
        Task<Z202_Contacto> UpdateContacto(Z202_Contacto contacto);
    }
}

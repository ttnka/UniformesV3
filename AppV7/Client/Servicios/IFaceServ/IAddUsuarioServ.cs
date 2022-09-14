using AppV7.Shared;

namespace AppV7.Client.Servicios.IFaceServ
{
    public interface IAddUsuarioServ
    {
        Task<EAddUsuario> AddUsuario(EAddUsuario addUsuario);
        Task<ERecupera> Recupera(ERecupera recupera);
        Task<EAddUsuario> ResetPassword(EAddUsuario eAddUsuario);
        
    }
}

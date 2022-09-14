using AppV7.Shared;

namespace AppV7.Server.Models.IFace
{
    public interface IAddUsuario
    {
        Task<EAddUsuario> AddUsuario(EAddUsuario addUsuario);
        Task<ERecupera> Recupera(ERecupera recupera);
        Task<EAddUsuario> NewPass(EAddUsuario newPass, string code);
        Task<EAddUsuario> ResetPassword(EAddUsuario eAddUsuario);

    }
}

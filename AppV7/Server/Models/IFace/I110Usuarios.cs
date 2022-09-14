using AppV7.Shared;

namespace AppV7.Server.Models.IFace
{
    public interface I110Usuarios
    {
        Task<IEnumerable<Z110_Usuarios>> Buscar(string clave, string orgX);
        Task<Z110_Usuarios> AddUsuario(Z110_Usuarios user);
        Task<Z110_Usuarios> UpDateUsuario(Z110_Usuarios user);
    }
}

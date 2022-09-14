using AppV7.Shared;

namespace AppV7.Client.Servicios.IFaceServ
{
    public interface I110UsuariosServ
    {
        Task<IEnumerable<Z110_Usuarios>> Buscar(string clave, string orgX);
        Task<Z110_Usuarios> AddUsuario(Z110_Usuarios usuario);
        Task<Z110_Usuarios> UpDateUsuario(Z110_Usuarios usuario);
    }
}

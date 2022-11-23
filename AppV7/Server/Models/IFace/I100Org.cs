using AppV7.Shared;

namespace AppV7.Server.Models.IFace
{
    public interface I100Org
    {
        Task<IEnumerable<Z100_Org>> Buscar(string clave);
        Task<Z100_Org> AddOrg(Z100_Org org);
        Task<Z100_Org> UpDateOrg(Z100_Org org);
    }
}

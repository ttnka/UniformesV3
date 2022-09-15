using AppV7.Shared;

namespace AppV7.Server.Models.IFace
{
    public interface I204ContDet
    {
        Task<IEnumerable<Z204_ContDet>> Buscar(string clave);
        Task<Z204_ContDet> AddContDet(Z204_ContDet contDet);
        Task<Z204_ContDet> UpdateContDet(Z204_ContDet contDet);
    }
}

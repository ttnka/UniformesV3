using AppV7.Shared;

namespace AppV7.Client.Servicios.IFaceServ
{
    public interface I204ContDetServ
    {
        Task<IEnumerable<Z204_ContDet>> Buscar(string clave);
        Task<Z204_ContDet> AddContDet(Z204_ContDet contDet);
        Task<Z204_ContDet> UpdateContDet(Z204_ContDet contDet);
    }
}

using AppV7.Shared;

namespace AppV7.Client.Servicios.IFaceServ
{
    public interface I232DetSolServ
    {
        /*
        Task<IEnumerable<Z232_DetSol>> FiltroSP(string FolioF, int EstadoF,
            string AlmacenF, string TipoEntradaF, string ComercioF, string ProductoF,
            string CiudadF, int CantidadF);
        */
        Task<IEnumerable<Z232_DetSol>> Buscar(string clave);
        Task<Z232_DetSol> AddDetalle(Z232_DetSol det);
        Task<Z232_DetSol> UpDateDetalle(Z232_DetSol det);
    }
}

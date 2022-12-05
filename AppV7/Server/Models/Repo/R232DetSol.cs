using AppV7.Client.Pages.Uniformes.Canano;
using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MySqlConnector;

//using System.Data.SqlClient;

namespace AppV7.Server.Models.Repo
{
    public class R232DetSol : I232DetSol
    {
        private readonly ApplicationDbContext _appDbContext;

        public R232DetSol(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Z232_DetSol> AddDetalle(Z232_DetSol det)
        {
            var res = await _appDbContext.DetSolicitud.AddAsync(det);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Z232_DetSol>> Buscar(string clave)
        {
            IQueryable<Z232_DetSol> querry = _appDbContext.DetSolicitud;
            switch (clave)
            {
                case "Alla":
                    querry = querry.Where(x => x.Estado == 1 && x.Status);
                    break;
                default:
                    string[] parametro = clave.Split("_-_");
                    Dictionary<string, string> ParamDic = new Dictionary<string, string>();
                    for (int i = 1; i < parametro.Length; i += 2)
                    {
                        if (!ParamDic.ContainsKey(parametro[i]))
                            ParamDic.Add(parametro[i], parametro[i + 1]);
                    }
                    switch (parametro[0])
                    {
                        case "Folio":
                            querry = querry.Where(x => x.Folio == ParamDic["Folio"] && x.Status);
                            break;
                        case "Inventarios":

                            MySqlParameter p1 = new MySqlParameter("@FoliosF", ParamDic["FoliosF"]);
                            MySqlParameter p2 = new MySqlParameter("@EstadoF", ParamDic["EstadoF"]);
                            MySqlParameter p3 = new MySqlParameter("@AlmacenF", ParamDic["AlmacenF"]);
                            MySqlParameter p4 = new MySqlParameter("@TipoEntradaF", ParamDic["TipoEntradaF"]);
                            MySqlParameter p5 = new MySqlParameter("@ComercioF", ParamDic["ComercioF"]);
                            MySqlParameter p6 = new MySqlParameter("@ProductoF", ParamDic["ProductoF"]);
                            MySqlParameter p7 = new MySqlParameter("@CiudadF", ParamDic["CiudadF"]);
                            MySqlParameter p8 = new MySqlParameter("@CantidadF", ParamDic["CantidadF"]);
                            
                            string ex = "Call DetFiltroGeneral(@FoliosF, @EstadoF, @AlmacenF, @TipoEntradaF,";
                            ex += "@ComercioF, @ProductoF, @CiudadF, @CantidadF)";
                            IEnumerable<Z232_DetSol> res = await _appDbContext.DetSolicitud.FromSqlRaw(ex,
                                        p1, p2, p3, p4, p5, p6, p7, p8).ToListAsync();
                            return res;
                            
                    }
                    break;
            }
            return await querry.ToListAsync();
        }
        /*
        public async Task<IEnumerable<Z232_DetSol>> FiltroSP(string FoliosF, int EstadoF, 
            string AlmacenF, string TipoEntradaF, string ComercioF, 
            string ProductoF, string CiudadF, int CantidadF)
        {
            MySqlParameter p1 = new MySqlParameter("@FoliosF", FoliosF);
            MySqlParameter p2 = new MySqlParameter("@EstadoF", EstadoF);
            MySqlParameter p3 = new MySqlParameter("@AlmacenF", AlmacenF);
            MySqlParameter p4 = new MySqlParameter("@TipoEntradaF", TipoEntradaF);
            MySqlParameter p5 = new MySqlParameter("@ComercioF", ComercioF);
            MySqlParameter p6 = new MySqlParameter("@ProductoF", ProductoF);
            MySqlParameter p7 = new MySqlParameter("@CiudadF", CiudadF);
            MySqlParameter p8 = new MySqlParameter("@CantidadF", CantidadF);
            string ex = "Call DetFiltroGeneral(@FoliosF, @EstadoF, @AlmacenF, @TipoEntradaF,";
            ex += "@ComercioF, @ProductoF, @CiudadF, @CantidadF)";

            var res = await _appDbContext.DetSolicitud.FromSqlRaw(ex, 
                p1, p2, p3, p4, p5, p6, p7, p8).ToListAsync();
            return res;
        }
        */
        public async Task<Z232_DetSol> UpDateDetalle(Z232_DetSol det)
        {
            var res = await _appDbContext.DetSolicitud.FirstOrDefaultAsync(
               e => e.DetId == det.DetId);

            if (res != null)
            {
                if (det.Status == false)
                {
                    res.Status = false;
                }
                else
                {
                    res.Folio = det.Folio;
                    res.Producto = det.Producto;
                    res.Cantidad = det.Cantidad;
                    res.Desc = det.Desc;
                    
                    res.Estado = det.Estado;
                    res.Status = det.Status;
                }
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                res = new Z232_DetSol();
            }
            return res;
        }
    }
}

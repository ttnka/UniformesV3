using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppV7.Server.Models.Repo
{
    public class R230Solicitud : I230Solicitud
    {
        private readonly ApplicationDbContext _appDbContext;

        public R230Solicitud(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Z230_Solicitud> AddSolicitud(Z230_Solicitud solicitud)
        {
            var res = await _appDbContext.Solicitudes.AddAsync(solicitud);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Z230_Solicitud>> Buscar(string clave)
        {
            IQueryable<Z230_Solicitud> querry = _appDbContext.Solicitudes.Include
                (x => x.DetSols);
            switch (clave)
            {
                case "Alla":
                    querry = querry.Where(x => x.Status);
                    querry = querry.OrderByDescending(x => x.Estado).ThenBy(x=>x.Fecha);
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
                        case "UserSol":
                            querry = querry.Where(x => x.Usuario == ParamDic["UserSol"] && x.Status);
                            break;
                        /*
                        case "Buscar":
                            var querry1 = _appDbContext.Almacenes.Include
                                (x => x.Solicitudes).ThenInclude(z => z.DetSols);
                            IEnumerable<Z210_Almacen> resultado = new List<Z210_Almacen>(); 
                            resultado = await querry1.ToListAsync();
                            return resultado;
                        /*
                        case "Rfc":
                            querry = querry.Where(x => x.Rfc == ParamDic["Rfc"]);
                            break;

                        default:
                            querry = querry.Where(x => x.OrgId == "Ninguno");
                            break;
                        */
                    }
                    
                    break;
            }
            return await querry.ToListAsync();
        }

        

        public async Task<Z230_Solicitud> UpDateSolicitud(Z230_Solicitud solicitud)
        {
            var res = await _appDbContext.Solicitudes.FirstOrDefaultAsync(
               e => e.SolicitudId == solicitud.SolicitudId);

            if (res != null)
            {
                if (solicitud.Status == false)
                {
                    res.Status = false;
                }
                else
                {
                    res.Fecha = solicitud.Fecha;
                    res.Folio = solicitud.Folio;
                    res.Almacen = solicitud.Almacen;
                    res.Usuario = solicitud.Usuario;
                    res.Tipo = solicitud.Tipo;
                    res.Desc = solicitud.Desc;
                    res.Estado = solicitud.Estado;
                    res.Status = solicitud.Status;
                }
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                res = new Z230_Solicitud();
            }
            return res;
        }
    }
}

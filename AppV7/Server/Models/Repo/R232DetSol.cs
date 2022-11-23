using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;

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
                            
                    }
                    break;
            }
            return await querry.ToListAsync();
        }

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

using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppV7.Server.Models.Repo
{
    public class R205DatosTipo : I205DatosTipo
    {
        private readonly ApplicationDbContext _appDbContext;

        public R205DatosTipo(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Z205_DatosTipo> AddDatosTipo(Z205_DatosTipo datosTipo)
        {
            var res = await _appDbContext.DatosTipo.AddAsync(datosTipo);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Z205_DatosTipo>> Buscar(string clave)
        {
            IQueryable<Z205_DatosTipo> querry = _appDbContext.DatosTipo;

            switch (clave)
            {
                case "All":
                    //return await querry.OrderByDescending(x => x.Fecha).ToListAsync();
                    break;
                    /*
                case "OrgX":
                    querry = querry.Where(x => x.OrgId == orgX && x.Sistema == false);
                    break;

                case "Sistema":
                    querry = orgX == "Vacio" ? querry.Where(x => x.Sistema == true) :
                        querry.Where(x => x.Sistema == true && x.OrgId == orgX);
                    break;
                case "Nombres":
                // IQueryable<Z190_Bitacora> querryN = _appDbContext.Bitacoras.
                //     Select(x => x.UsuariosId);

                // break;
                default:

                    break;
                    */
            }

            return await querry.ToListAsync();
        }

        public async Task<Z205_DatosTipo> UpdateDatosTipo(Z205_DatosTipo datosTipo)
        {
            var res = await _appDbContext.DatosTipo.
                FirstOrDefaultAsync(e => e.Id == datosTipo.Id);

            if (res != null)
            {
                if (datosTipo.Status == false)
                {
                    res.Status = false;
                }
                else
                {
                    res.Grupo = datosTipo.Grupo;
                    res.Texto = datosTipo.Texto;
                    
                    res.Estado = datosTipo.Estado;
                    res.Status = datosTipo.Status;
                }
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                res = new Z205_DatosTipo();
            }
            return res;
        }
    }
}

using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppV7.Server.Models.Repo
{
    public class R204ContDet : I204ContDet
    {
        private readonly ApplicationDbContext _appDbContext;

        public R204ContDet(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Z204_ContDet> AddContDet(Z204_ContDet contDet)
        {
            var res = await _appDbContext.ContDet.AddAsync(contDet);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Z204_ContDet>> Buscar(string clave)
        {
            IQueryable<Z204_ContDet> querry = _appDbContext.ContDet;

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

        public async Task<Z204_ContDet> UpdateContDet(Z204_ContDet contDet)
        {
            var res = await _appDbContext.ContDet.
                FirstOrDefaultAsync(e => e.Id == contDet.Id);

            if (res != null)
            {
                if (contDet.Status == false)
                {
                    res.Status = false;
                }
                else
                {
                    res.ContactoId = contDet.ContactoId;
                    res.TipoId = contDet.TipoId;
                    res.Texto = contDet.Texto;
                    
                    res.Estado = contDet.Estado;
                    res.Status = contDet.Status;
                }
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                res = new Z204_ContDet();
            }
            return res;
        }
    }
}

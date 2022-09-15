using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppV7.Server.Models.Repo
{
    public class R195MailUs : I195MailUs
    {
        private readonly ApplicationDbContext _appDbContext;

        public R195MailUs(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Z195_MailUs> AddMailUs(Z195_MailUs mailUs)
        {
            var res = await _appDbContext.MailUs.AddAsync(mailUs);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Z195_MailUs>> Buscar(string clave)
        {
            IQueryable<Z195_MailUs> querry = _appDbContext.MailUs;

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
    }
}

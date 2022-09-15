using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppV7.Server.Models.Repo
{
    public class R800WebSite : I800WebSite
    {
        private readonly ApplicationDbContext _appDbContext;

        public R800WebSite(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Z800_WebSite> AddWebSite(Z800_WebSite webSite)
        {
            var res = await _appDbContext.WebSite.AddAsync(webSite);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Z800_WebSite>> Buscar(string clave)
        {
            IQueryable<Z800_WebSite> querry = _appDbContext.WebSite;

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

        public async Task<Z800_WebSite> UpdateWebSite(Z800_WebSite webSite)
        {
            var res = await _appDbContext.WebSite.
                FirstOrDefaultAsync(e => e.Id == webSite.Id);

            if (res != null)
            {
                if (webSite.Status == false)
                {
                    res.Status = false;
                }
                else
                {
                    res.Titulo = webSite.Titulo;
                    res.Padre = webSite.Padre;
                    res.Visible = webSite.Visible;
                    res.Fecha = webSite.Fecha;

                    res.Estado = webSite.Estado;
                    res.Status = webSite.Status;
                }
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                res = new Z800_WebSite();
            }
            return res;
        }
    }
}

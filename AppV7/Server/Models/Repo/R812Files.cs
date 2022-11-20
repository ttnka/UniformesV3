using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppV7.Server.Models.Repo
{
    public class R812Files : I812Files
    {
        private readonly ApplicationDbContext _appDbContext;

        public R812Files(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Z812_Files> AddFile(Z812_Files files)
        {
            var res = await _appDbContext.FilesWeb.AddAsync(files);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Z812_Files>> Buscar(string clave)
        {
            IQueryable<Z812_Files> querry = _appDbContext.FilesWeb;

            switch (clave)
            {
                case "All":
                    //return await querry.OrderByDescending(x => x.Fecha).ToListAsync();
                    break;
                case "Alla":
                    querry = querry.Where(x => x.Estado == 1 && x.Status); 
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

        public async Task<Z812_Files> UpDateFile(Z812_Files files)
        {
            var res = await _appDbContext.FilesWeb.
                FirstOrDefaultAsync(e => e.FileId == files.FileId);

            if (res != null)
            {
                if (files.Status == false)
                {
                    res.Status = false;
                }
                else
                {
                    res.Fuente = files.Fuente;
                    res.FuenteId = files.FuenteId;
                    res.Tipo = files.Tipo;
                    res.Archivo = files.Archivo;
                    res.Gpo = files.Gpo;
                    res.Estado = files.Estado;
                    res.Status = files.Status;
                }
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                res = new Z812_Files();
            }
            return res;
        }
    }
}

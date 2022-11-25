using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppV7.Server.Models.Repo
{
    public class R290Meta : I290Meta
    {
        private readonly ApplicationDbContext _appDbContext;

        public R290Meta(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Z290_Meta> AddMeta(Z290_Meta meta)
        {
            var res = await _appDbContext.Metas.AddAsync(meta);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Z290_Meta>> Buscar(string clave)
        {
            IQueryable<Z290_Meta> querry = _appDbContext.Metas;

            switch (clave)
            {
                case "All":

                    break;
                    /*
                case "Alla":
                    querry = querry.Where(x => x.Estado == 1 && x.Status);
                    querry = querry.OrderBy(x => x.OldEmail);
                    break;
                case "Allo":
                    return await querry.OrderBy(x => x.OrgId).ToListAsync();

                case "Org":
                    querry = querry.Where(x => x.OrgId == orgX);
                    break;

                case "OrgX":
                    querry = querry.Where(x => x.OrgId == orgX && x.Status == true);
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
                        case "UserId":
                            querry = querry.Where(x => x.UsuariosId == ParamDic["UserId"]);
                            return await querry.ToListAsync();

                        case "OldEmail":
                            querry = querry.Where(x => x.OldEmail == ParamDic["OldEmail"]);
                            return await querry.ToListAsync();
                    }

                    break;
                    */
            }
            return await querry.ToListAsync();
        }

        public async Task<Z290_Meta> UpDateMeta(Z290_Meta meta)
        {
            var res = await _appDbContext.Metas.FirstOrDefaultAsync(e => e.MetaId == meta.MetaId);
            if (res != null)
            {
                if (meta.Status == false)
                {
                    res.Status = false;
                }
                else
                {
                    res.Titulo = meta.Titulo;
                    res.Desc = meta.Desc;
                    res.Tipo = meta.Tipo;
                    res.Cantidad = meta.Cantidad;
                    
                    res.Usuario = meta.Usuario;
                    res.Municipio = meta.Municipio;
                    
                    res.Estado = meta.Estado;
                    res.Status = meta.Status;
                }
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                res = new Z290_Meta();
            }
            return res;
        }
    }
}

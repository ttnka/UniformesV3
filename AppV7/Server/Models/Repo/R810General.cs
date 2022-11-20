using AppV7.Client.Pages.Usuarios.Web;
using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace AppV7.Server.Models.Repo
{
    public class R810General : I810General  
    {
        private readonly ApplicationDbContext _appDbContext;

        public R810General(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Z810_General> AddGeneral(Z810_General gral)
        {
            var res = await _appDbContext.GeneralWeb.AddAsync(gral);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Z810_General>> Buscar(string clave)
        {
            IQueryable<Z810_General> querry = _appDbContext.GeneralWeb;

            switch (clave)
            {
                case "All":
                    return await querry.OrderByDescending(x => x.Fecha).ToListAsync();
                    
                case "Alla":
                    querry = querry.Where(x => x.Status == true);
                    querry = querry.OrderByDescending(x => x.Fecha);
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
                        case "Allo":
                            querry = querry.Where(x => x.Org == ParamDic["Org"]);
                            break;
                        case "AlloS":
                            querry = querry.Where(x => x.Org == ParamDic["Org"] &&
                                    x.Status == true);
                            break;
                            /*
                        case "OldEMail":
                            querry = querry.Where(x => x.OldEmail == ParamDic["OldEmail"]);
                            break;
                            */
                    }

                    break;
                    /*
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

        public async Task<Z810_General> UpDateGeneral(Z810_General gral)
        {
            var res = await _appDbContext.GeneralWeb.
                FirstOrDefaultAsync(e => e.GeneralId == gral.GeneralId);

            if (res != null)
            {
                if (gral.Status == false)
                {
                    res.Status = false;
                }
                else
                {
                    res.GeneralId = gral.GeneralId;
                    res.Pagina = gral.Pagina;
                    res.Titulo = gral.Titulo;
                    res.Subtitulo = gral.Subtitulo;
                    res.Nota = gral.Nota;
                    res.Comentario = gral.Comentario;
                    res.Org = gral.Org;
                    res.Estado = gral.Estado;
                    res.Status = gral.Status;
                }
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                res = new Z810_General();
            }
            return res;
        }
    }
}

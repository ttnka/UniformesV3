using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppV7.Server.Models.Repo
{
    public class R802WebConfig : I802WebConfig
    {
        private readonly ApplicationDbContext _appDbContext;

        public R802WebConfig(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Z802_WebConfig> AddWebConfig(Z802_WebConfig wConfig)
        {
            var res = await _appDbContext.WebConfig.AddAsync(wConfig);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Z802_WebConfig>> Buscar(string clave)
        {
            IQueryable<Z802_WebConfig> querry = _appDbContext.WebConfig;
            switch (clave)
            {
                case "All":

                    break;
                    /*
                    case "Allo":
                        return await querry.OrderBy(x=>x.OrgId).ToListAsync();

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
                            case "wConfigId":
                                querry = querry.Where(x => x.UsuariosId == ParamDic["wConfigId"]);
                                break;
                            case "OldEMail":
                                querry = querry.Where(x => x.OldEmail == ParamDic["OldEmail"]);
                                break;
                        }

                        break;
                    */
            }
            return await querry.ToListAsync();
        }

        public async Task<Z802_WebConfig> UpDateWebConfig(Z802_WebConfig wConfig)
        {
            var res = await _appDbContext.WebConfig.FirstOrDefaultAsync(e => e.Id == wConfig.Id);
            if (res != null)
            {
                if (wConfig.Status == false)
                {
                    res.Status = false;
                }
                else
                {
                    res.Pagina = wConfig.Pagina;
                    res.Componente = wConfig.Componente;
                    res.Titulo = wConfig.Titulo;
                    res.SubTitulo = wConfig.SubTitulo;
                    res.Foto = wConfig.Foto;
                    res.Archivo = wConfig.Archivo;
                    res.Carrucel = wConfig.Carrucel;
                    res.Nota1 = wConfig.Nota1;
                    res.Nota2 = wConfig.Nota2;
                    res.Nota3 = wConfig.Nota3;
                    res.NotaEspecial = wConfig.NotaEspecial;

                    res.Publicidad1 = wConfig.Publicidad1;
                    res.Publicidad2 = wConfig.Publicidad2;
                    res.Publicidad3 = wConfig.Publicidad3;
                    res.PublicidadEspecial = wConfig.PublicidadEspecial;

                    res.Estado = wConfig.Estado;
                    res.Status = wConfig.Status;
                }
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                res = new Z802_WebConfig();
            }
            return res;
        }
    }
}

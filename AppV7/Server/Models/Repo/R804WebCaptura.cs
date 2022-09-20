using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace AppV7.Server.Models.Repo
{
    public class R804WebCaptura : I804WebCaptura
    {
        private readonly ApplicationDbContext _appDbContext;

        public R804WebCaptura(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Z804_WebCaptura> AddWebCaptura(Z804_WebCaptura webCaptura)
        {
            var res = await _appDbContext.WebCaptura.AddAsync(webCaptura);
            await _appDbContext.SaveChangesAsync();
            return res.Entity; 
        }

        public async Task<IEnumerable<Z804_WebCaptura>> Buscar(string clave)
        {
            IQueryable<Z804_WebCaptura> querry = _appDbContext.WebCaptura;
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

        public async Task<Z804_WebCaptura> UpdateWebCaptura(Z804_WebCaptura webCaptura)
        {
            var res = await _appDbContext.WebCaptura.FirstOrDefaultAsync(e => e.CapturaId == webCaptura.CapturaId);
            if (res != null)
            {
                if (webCaptura.Status == false)
                {
                    res.Status = false;
                }
                else
                {
                    res.Pagina = webCaptura.Pagina;
                    
                    res.Titulo = webCaptura.Titulo;
                    res.SubTitulo = webCaptura.SubTitulo;
                    
                    res.Nota1 = webCaptura.Nota1;
                    res.Nota2 = webCaptura.Nota2;
                    res.Nota3 = webCaptura.Nota3;
                    res.NotaEspecial = webCaptura.NotaEspecial;

                    res.Publicidad1 = webCaptura.Publicidad1;
                    res.Publicidad2 = webCaptura.Publicidad2;
                    res.Publicidad3 = webCaptura.Publicidad3;
                    res.PublicidadEspecial = webCaptura.PublicidadEspecial;

                    res.Estado = webCaptura.Estado;
                    res.Status = webCaptura.Status;
                }
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                res = new Z804_WebCaptura();
            }
            return res;
        }
    }
}

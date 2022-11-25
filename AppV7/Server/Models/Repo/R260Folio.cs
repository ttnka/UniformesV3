using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppV7.Server.Models.Repo
{
    public class R260Folio : I260Folio
    {
        private readonly ApplicationDbContext _appDbContext;

        public R260Folio(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Z260_Folio> AddFolio(Z260_Folio folio)
        {
            var res = await _appDbContext.Folios.AddAsync(folio);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Z260_Folio>> Buscar(string clave)
        {
            IQueryable<Z260_Folio> querry = _appDbContext.Folios;

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
                        case "folioId":
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

        public async Task<Z260_Folio> UpDateFolio(Z260_Folio folio)
        {
            var res = await _appDbContext.Folios.FirstOrDefaultAsync(e => e.FolioId == folio.FolioId);
            if (res != null)
            {
                res.RegId = folio.RegId;
                res.FechaEntrega = folio.FechaEntrega;
                res.Status = folio.Status;
                res.Folio = folio.Folio;
                res.NombreCompleto = folio.NombreCompleto;
                res.Curp = folio.Curp;
                res.TurnoId = folio.TurnoId;
                res.Grado = folio.Status;
                res.Codigo = folio.Codigo;
                res.EscuelaId = folio.EscuelaId;
                res.InscStatusId = folio.InscStatusId;
                res.GeneroId = folio.GeneroId;
                res.NivelId = folio.NivelId;
                res.TipoValId = folio.TipoValId;
                res.Localidad = folio.Localidad;
                res.Municipio = folio.Municipio;
            
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                res = new Z260_Folio();
            }
            return res;
        }
    }
}

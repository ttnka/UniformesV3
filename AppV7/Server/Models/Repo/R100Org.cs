using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppV7.Server.Models.Repo
{
    public class R100Org : I100Org
    {
        private readonly ApplicationDbContext _appDbContext;

        public R100Org(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Z100_Org> AddOrg(Z100_Org org)
        {
            var res = await _appDbContext.Organizaciones.AddAsync(org);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Z100_Org>> Buscar(string clave)
        {
            IQueryable<Z100_Org> querry = _appDbContext.Organizaciones;
            switch (clave)
            {
                case "All":
                    querry = querry.OrderByDescending(x => x.Status).ThenBy(z => z.Comercial);
                    return await querry.ToListAsync();

                case "Activos":
                case "Suspendidos":

                    bool AS = clave == "Activos" ? true : false;
                        querry = querry.Where(x => x.Status == AS);
                    
                    return await querry.ToListAsync();
            }

            string[] parametro = clave.Split("_-_");
            Dictionary<string, string> ParamDic = new Dictionary<string, string>();
            for (int i = 1; i < parametro.Length; i += 2)
            {
                if (!ParamDic.ContainsKey(parametro[i]))
                    ParamDic.Add(parametro[i], parametro[i + 1]);
                
            }
            
            switch (parametro[0])
            {
                case "Moral":
                    bool person = ParamDic["Moral"] == "1" ? true : false;
                    querry = querry.Where(x => x.Moral == person);
                    break;
                case "Uno":
                    querry = querry.Where(x => x.OrgId == ParamDic["OrgId"]);
                    
                    break;
                case "Rfc":
                    querry = querry.Where(x => x.Rfc == ParamDic["Rfc"]);
                    break;

                default:
                    querry = querry.Where(x => x.OrgId == "Ninguno");
                    break;
            }
            
            return await querry.ToListAsync();

        }

        public async Task<Z100_Org> UpDateOrg(Z100_Org org)
        {
            var res = await _appDbContext.Organizaciones.
                FirstOrDefaultAsync(e => e.OrgId == org.OrgId);

            if (res != null)
            {
                if (org.Status == false)
                {
                    res.Status = false;
                }
                else
                {
                    res.Rfc = org.Rfc;
                    res.Comercial = org.Comercial;
                    res.Moral = org.Moral;
                    res.WebAdmin = org.WebAdmin;

                    res.RazonSocial = org.RazonSocial;
                    res.Nombre = org.Nombre;
                    res.Paterno = org.Paterno;
                    res.Materno = org.Materno;
                    
                    res.Estado = org.Estado;
                    res.Status = org.Status;
                }
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                res = new Z100_Org();
            }
            return res;
        }
    }
}

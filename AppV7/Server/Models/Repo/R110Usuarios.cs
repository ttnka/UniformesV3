using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppV7.Server.Models.Repo
{
    public class R110Usuarios : I110Usuarios
    {
        private readonly ApplicationDbContext _appDbContext;

        public R110Usuarios(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Z110_Usuarios> AddUsuario(Z110_Usuarios user)
        {
            var res = await _appDbContext.Usuarios.AddAsync(user);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Z110_Usuarios>> Buscar(string clave, string orgX)
        {
            IQueryable<Z110_Usuarios> querry = _appDbContext.Usuarios;
            
            switch (clave)
            {
                case "All":
                    
                    break;
                case "Alla":
                    querry = querry.Where(x => x.Estado == 1 && x.Status);
                    querry = querry.OrderBy(x => x.OldEmail);
                    break;
                case "Allo":
                    return await querry.OrderBy(x=>x.OrgId).ToListAsync();
                    
                case "Org":
                    querry = querry.Where(x => x.OrgId == orgX);
                    break;
            
                case "OrgX":
                    querry = querry.Where(x => x.OrgId == orgX && x.Status);
                    break;
                case "Afiliado":
                case "Afiliados":
                    querry = querry.Where(x => x.Nivel == 2 && x.Status);
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
            }
            return await querry.ToListAsync();
        }

        public async Task<Z110_Usuarios> UpDateUsuario(Z110_Usuarios user)
        {
            var res = await _appDbContext.Usuarios.FirstOrDefaultAsync(e => e.UsuariosId == user.UsuariosId);
            if (res != null)
            {
                if (user.Status == false)
                {
                    res.Status = false;
                }
                else
                {
                    res.Nombre = user.Nombre;
                    res.Paterno = user.Paterno;
                    res.Materno = user.Materno;
                    res.OrgId = user.OrgId;
                    res.Nivel = user.Nivel;
                    res.OldEmail = user.OldEmail;
                    res.Municipio = user.Municipio;
                    res.Estado = user.Estado;
                    res.Status = user.Status;
                }
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                res = new Z110_Usuarios();
            }
            return res;
        }
    }
}

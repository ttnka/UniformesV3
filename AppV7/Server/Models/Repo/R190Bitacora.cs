using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppV7.Server.Models.Repo
{
    public class R190Bitacora : I190Bitacora
    {
        private readonly ApplicationDbContext _appDbContext;

        public R190Bitacora(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Z190_Bitacora> AddBitacora(Z190_Bitacora bitacora)
        {
            var res = await _appDbContext.Bitacoras.AddAsync(bitacora);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Z190_Bitacora>> Buscar(string clave, string orgX)
        {
            IQueryable<Z190_Bitacora> querry = _appDbContext.Bitacoras;

            switch (clave)
            {
                case "All":
                    //return await querry.OrderByDescending(x => x.Fecha).ToListAsync();
                    break;

                case "OrgX":
                    querry = querry.Where(x => x.OrgId == orgX && x.Sistema == false  );
                    break;

                case "Sistema": 
                    querry = orgX == "Vacio" ? querry.Where(x => x.Sistema == true) :
                        querry.Where (x => x.Sistema == true && x.OrgId == orgX); 
                    break;
                case "Comerciante":
                    querry = querry.Where(x=> x.UsuariosId == orgX);  
                    break;
                /*
                default:
                    
                    break;
                */
            }

            return await querry.OrderByDescending(x => x.Fecha).ToListAsync();
        }
    }
}

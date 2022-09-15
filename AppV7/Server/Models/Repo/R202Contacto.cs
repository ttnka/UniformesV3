using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppV7.Server.Models.Repo
{
    public class R202Contacto : I202Contacto
    {
        private readonly ApplicationDbContext _appDbContext;

        public R202Contacto(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Z202_Contacto> AddContacto(Z202_Contacto contacto)
        {
            var res = await _appDbContext.Contactos.AddAsync(contacto);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Z202_Contacto>> Buscar(string clave)
        {
            IQueryable<Z202_Contacto> querry = _appDbContext.Contactos;

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

        public async Task<Z202_Contacto> UpdateContacto(Z202_Contacto contacto)
        {
            var res = await _appDbContext.Contactos.
                FirstOrDefaultAsync(e => e.Id == contacto.Id);

            if (res != null)
            {
                if (contacto.Status == false)
                {
                    res.Status = false;
                }
                else
                {
                    res.OrgId = contacto.OrgId;
                    res.UsuarioId = contacto.UsuarioId;
                    res.UsuarioName = contacto.UsuarioName;
                    res.tipo = contacto.tipo;
                    res.Puesto = contacto.Puesto;
                    res.Estado = contacto.Estado;
                    res.Status = contacto.Status;
                }
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                res = new Z202_Contacto();
            }
            return res;
        }
    }
}

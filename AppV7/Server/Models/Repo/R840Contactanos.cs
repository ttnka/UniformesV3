using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppV7.Server.Models.Repo
{
    public class R840Contactanos : I840Contactanos
    {
        private readonly ApplicationDbContext _appDbContext;

        public R840Contactanos(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Z840_Contactanos> AddContactanos(Z840_Contactanos contactanos)
        {
            var res = await _appDbContext.Contactanos.AddAsync(contactanos);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Z840_Contactanos>> Buscar(string clave)
        {
            IQueryable<Z840_Contactanos> querry = _appDbContext.Contactanos;

            switch (clave)
            {
                case "All":
                    //return await querry.OrderByDescending(x => x.Fecha).ToListAsync();
                    break;
                   
                case "General":
                    querry = querry.Where(x => x.General && x.Status);
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

        public async Task<Z840_Contactanos> UpdateContactanos(Z840_Contactanos contactanos)
        {
            var res = await _appDbContext.Contactanos.
                FirstOrDefaultAsync(e => e.Id == contactanos.Id);

            if (res != null)
            {
                if (contactanos.Status == false)
                {
                    res.Status = false;
                }
                else
                {
                    res.General = contactanos.General;
                    res.Formato = contactanos.Formato;
                    res.ParaMail = contactanos.ParaMail;
                    res.Mapa = contactanos.Mapa;
                    res.Titulo = contactanos.Titulo;
                    res.Latitud = contactanos.Latitud;
                    res.Longitud = contactanos.Longitud;
                    res.Comentario = contactanos.Comentario;
                    res.Directorio = contactanos.Directorio;
                    res.Foto = contactanos.Foto;
                    res.Nombre = contactanos.Nombre;
                    res.Puesto = contactanos.Puesto;
                    res.Mail = contactanos.Mail;
                    res.Tel = contactanos.Tel;
                    res.Cel = contactanos.Cel;
                    res.Domicilio = contactanos.Domicilio;
                    res.Telefonos = contactanos.Telefonos;

                    res.Estado = contactanos.Estado;
                    res.Status = contactanos.Status;
                }
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                res = new Z840_Contactanos();
            }
            return res;
        }
    }
}

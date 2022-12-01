using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppV7.Server.Models.Repo
{
    public class R210Almacen : I210Almacen
    {
        private readonly ApplicationDbContext _appDbContext;

        public R210Almacen(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Z210_Almacen> AddAlmacen(Z210_Almacen almacen)
        {
            var res = await _appDbContext.Almacenes.AddAsync(almacen);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Z210_Almacen>> Buscar(string clave)
        {
            IQueryable<Z210_Almacen> querry = _appDbContext.Almacenes;
            switch (clave)
            {
                case "Alla":

                    querry = querry.Where(x => x.Estado == 1 && x.Status);
                    break;

                case "Full":
                    //string mpio = "Alla"; string alma = "Alla";
                    //string comer = "Alla"; string prod = "Alla";
                    querry = _appDbContext.Almacenes
                        .Include(y => y.Solicitudes)
                        .ThenInclude(z => z.DetSols);
                    /*
                    querry = querry.Where(x =>
                    (mpio == "Alla" ? x.Municipio != null : x.Municipio == mpio) &
                    (alma == "Alla" ? x.AlmacenId != null : x.AlmacenId == alma));
                    
                    querry = querry.Where(y.Solicitudes =>
                    (comer == "Alla" ? y.Solicitudes.Select(z => z.Usuario !=null) : 
                        y.Solicitudes.Select(z => z.Usuario == comer))
                    );
                    */
                    break;
                default:
                    querry = querry.Where(x => x.Estado == 0);
                    break;
            }
            return await querry.ToListAsync();
        }

        public async Task<Z210_Almacen> UpDateAlmacen(Z210_Almacen almacen)
        {
            var res = await _appDbContext.Almacenes.FirstOrDefaultAsync(
                e => e.AlmacenId == almacen.AlmacenId);

            if (res != null)
            {
                if (almacen.Status == false)
                {
                    res.Status = false;
                }
                else
                {
                    res.Corto = almacen.Corto;
                    res.Nombre = almacen.Nombre;
                    res.Desc = almacen.Desc;
                    res.Domicilio = almacen.Domicilio;
                    res.Municipio = almacen.Municipio;

                    res.Estado = almacen.Estado;
                    res.Status = almacen.Status;
                }
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                res = new Z210_Almacen();
            }
            return res;
        }
    }
}

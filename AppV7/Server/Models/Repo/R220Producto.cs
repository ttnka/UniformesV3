using AppV7.Client.Pages.Uniformes.Canano;
using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppV7.Server.Models.Repo
{
    public class R220Producto : I220Producto
    {
        private readonly ApplicationDbContext _appDbContext;

        public R220Producto(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Z220_Producto> AddProducto(Z220_Producto producto)
        {
            var res = await _appDbContext.Productos.AddAsync(producto);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Z220_Producto>> Buscar(string clave)
        {
            IQueryable<Z220_Producto> querry = _appDbContext.Productos;
            switch (clave)
            {
                case "Alla":
                    querry = querry.Where(x => x.Estado == 1 && x.Status);
                    break;
            }
            return await querry.ToListAsync();
        }

        public async Task<Z220_Producto> UpDateProducto(Z220_Producto producto)
        {
            var res = await _appDbContext.Productos.FirstOrDefaultAsync(
               e => e.ProductoId == producto.ProductoId);

            if (res != null)
            {
                if (producto.Status == false)
                {
                    res.Status = false;
                }
                else
                {
                    res.Corto = producto.Corto;
                    res.Nombre = producto.Nombre;
                    res.Desc = producto.Desc;
                    res.Grupo = producto.Grupo;
                    res.Talla = producto.Talla;

                    res.Estado = producto.Estado;
                    res.Status = producto.Status;
                }
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                res = new Z220_Producto();
            }
            return res;
        }
    }
}

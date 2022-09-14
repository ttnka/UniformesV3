using AppV7.Server.Data;
using AppV7.Shared;

namespace AppV7.Server.Librerias
{
    public class MisFunc
    {
        private readonly ApplicationDbContext _appDbContext;

        public MisFunc(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Z190_Bitacora WriteBitacora(string userId, string orgId, string desc,
            bool sistema)
        {
            Z190_Bitacora bitacora = new Z190_Bitacora();
            bitacora.UsuariosId = userId;
            bitacora.OrgId = orgId;
            bitacora.Desc = desc;
            bitacora.Sistema = sistema;

            return bitacora;
        }
        public bool EscribirBitacora(string userId, string orgId, string desc,
            bool sistema)
        {
            Z190_Bitacora bitacora = new Z190_Bitacora();
            bitacora.UsuariosId = userId;
            bitacora.OrgId = orgId;
            bitacora.Desc = desc;
            bitacora.Sistema = sistema;
            _appDbContext.Bitacoras.AddRangeAsync(bitacora);
            _appDbContext.SaveChangesAsync();

            return true;
        }
    }
}

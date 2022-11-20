using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.AspNetCore.Components;

namespace AppV7.Server.Models.Repo
{
    public class RRegistroInicial : IRegistroInicial
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly I100Org _orgsIServ;
        private readonly I110Usuarios _usersIServ;
        private readonly IAddUsuario _addUser;

        public RRegistroInicial(ApplicationDbContext appDbContext, 
            I100Org orgsIServ, I110Usuarios usersIServ, IAddUsuario addUser)
        {
            _appDbContext = appDbContext;
            _orgsIServ = orgsIServ;
            _usersIServ = usersIServ;
            _addUser = addUser;
        }

        public async Task<bool> DoRegInicial()
        {
            var registro = await _orgsIServ.Buscar($"Rfc_-_Rfc_-_{Constantes.SyRfc}");
            if (registro.Count() != 0) return false;
            
                // Genera una nueva organizacion con datos sistema 
                Z100_Org SysOrg = new();
                SysOrg.Rfc = Constantes.SyRfc;
                SysOrg.Comercial = Constantes.SyRazonSocial;
                SysOrg.Moral = true;
                SysOrg.RazonSocial = Constantes.SyRazonSocial;
                SysOrg.Estado = Constantes.SyEstado;
                SysOrg.Status = Constantes.SyStatus;
                var newSysOrg = await _orgsIServ.AddOrg(SysOrg);

                // Genera un nuevo acceso al sistema con un usuario
                EAddUsuario eAddUsuario = new EAddUsuario();
                eAddUsuario.Mail = Constantes.SyMail;
                eAddUsuario.Pass = Constantes.SysPassword;
                eAddUsuario.OrgId = newSysOrg.OrgId;
                eAddUsuario.Nivel = 7;
                var userNew = await _addUser.AddUsuario(eAddUsuario);

                // Actualiza los datos del usuario al que se le genero el acceso 
                Z110_Usuarios SyUser = new();
                SyUser.UsuariosId = userNew.UsuarioId!;
                SyUser.OrgId = userNew.OrgId!;
                SyUser.Nombre = Constantes.SyRazonSocial;
                SyUser.Paterno = Constantes.ElDominio;
                SyUser.OldEmail = Constantes.SyMail;
                SyUser.Nivel = eAddUsuario.Nivel;
                SyUser.Estado = Constantes.SyEstado;
                SyUser.Status = true;
                var pruba = await _usersIServ.UpDateUsuario(SyUser);

                // Genera una organizacion nueva para publico en general
                Z100_Org PgOrg = new();
                PgOrg.Rfc = Constantes.PgRfc;
                PgOrg.Comercial = Constantes.PgRazonSocial;
                PgOrg.Moral = true;
                PgOrg.RazonSocial = Constantes.PgRazonSocial;
                PgOrg.Estado = Constantes.PgEstado;
                PgOrg.Status = Constantes.PgStatus;
                var newPgOrg = await _orgsIServ.AddOrg(PgOrg);

                // Genera acceso para publico en general todos 
                EAddUsuario eAddUsuarioPublico = new EAddUsuario();
                eAddUsuarioPublico.Mail = Constantes.DeMailPublico;
                eAddUsuarioPublico.Pass = Constantes.PasswordMailPublico;
                eAddUsuarioPublico.OrgId = newPgOrg.OrgId;
                eAddUsuarioPublico.Nivel = Constantes.NivelPublico;
                var userNewPublico = await _addUser.AddUsuario(eAddUsuarioPublico);
                
                // Genera el usuario al que se le genero el acceso 
                Z110_Usuarios PgUser = new();
                PgUser.UsuariosId = userNewPublico.UsuarioId!;
                PgUser.OrgId = userNewPublico.OrgId!;
                PgUser.Nombre = Constantes.PgRazonSocial;
                PgUser.Paterno = Constantes.ElDominio;
                PgUser.OldEmail = Constantes.DeMailPublico;
                PgUser.Nivel = Constantes.NivelPublico;
                PgUser.Estado = Constantes.NivelPublico;
                PgUser.Status = true;
                var pu2 = await _usersIServ.UpDateUsuario(PgUser);

            
            return true;
        }
    }
}

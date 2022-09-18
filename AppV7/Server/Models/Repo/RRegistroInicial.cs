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
            if (registro.Count() == 0)
            {
                Z100_Org SysOrg = new();
                SysOrg.Rfc = Constantes.SyRfc;
                SysOrg.Comercial = Constantes.SyRazonSocial;
                SysOrg.Moral = true;
                SysOrg.RazonSocial = Constantes.SyRazonSocial;
                SysOrg.Estado = Constantes.SyEstado;
                SysOrg.Status = Constantes.SyStatus;
                var newSysOrg = await _orgsIServ.AddOrg(SysOrg);

                EAddUsuario eAddUsuario = new EAddUsuario();
                eAddUsuario.Mail = Constantes.SyMail;
                eAddUsuario.Pass = Constantes.SysPassword;
                eAddUsuario.OrgId = newSysOrg.OrgId;
                eAddUsuario.Nivel = 7;
                var userNew = await _addUser.AddUsuario(eAddUsuario);

                Z110_Usuarios SyUser = new();
                SyUser.UsuariosId = userNew.UsuarioId!;
                SyUser.OrgId = userNew.OrgId!;
                SyUser.Nombre = Constantes.SyRazonSocial;
                SyUser.Paterno = Constantes.ElDominio;
                SyUser.OldEmail = Constantes.SyMail;
                SyUser.Nivel = eAddUsuario.Nivel;
                SyUser.Estado = Constantes.SyEstado;
                SyUser.Status = true;
                await _usersIServ.UpDateUsuario(SyUser);

                Z100_Org PgOrg = new();
                PgOrg.Rfc = Constantes.PgRfc;
                PgOrg.Comercial = Constantes.PgRazonSocial;
                PgOrg.Moral = true;
                PgOrg.RazonSocial = Constantes.PgRazonSocial;
                PgOrg.Estado = Constantes.PgEstado;
                PgOrg.Status = Constantes.PgStatus;
                await _orgsIServ.AddOrg(PgOrg);

            }
            
            return true;
        }
    }
}

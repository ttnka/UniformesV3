using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace AppV7.Server.Models.Repo
{
    public class RAddUsuario : IAddUsuario 
    {
        private readonly ApplicationDbContext _appDbContext;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly IEmailSender _emailSender;
        private readonly IEnviarMails _enviarMails;
        private readonly NavigationManager _navigationManager;
        private readonly I190Bitacora _bitacoraIServ;
        
        private Z110_Usuarios NewUsuario { get; set; } = new();
        public RAddUsuario(ApplicationDbContext appDbContext,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IEnviarMails enviarMails,
            NavigationManager navigationManager,
            I190Bitacora bitacoraIServ)
        {
            _appDbContext = appDbContext;
            
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _emailSender = emailSender;
            _enviarMails = enviarMails;
            _navigationManager = navigationManager; 
            _bitacoraIServ = bitacoraIServ;
           
        }
        public async Task<EAddUsuario> ResetPassword(EAddUsuario eAddUsuario)
        {
            var user = await _userManager.FindByEmailAsync(eAddUsuario.Mail);
            eAddUsuario.Positivo = false;
            eAddUsuario.NewPass = false;
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, 
                    eAddUsuario.ConfirmacionCode, eAddUsuario.Pass);
                if (result.Succeeded)
                {
                    eAddUsuario.Positivo = true;
                    eAddUsuario.NewPass = true;
                }
            }
            return eAddUsuario;
        }
        public async Task<EAddUsuario> NewPass(EAddUsuario newPass, string code)
        {
            var user = await _userManager.FindByEmailAsync(newPass.Mail);
            newPass.Positivo = false;
            newPass.NewPass = false;
            if (user != null) 
            {
                var result = await _userManager.ResetPasswordAsync(user, code, newPass.Pass);
                if (result.Succeeded)
                {
                    newPass.Positivo = true;
                    newPass.NewPass = true;
                }
            }
            return newPass;
        }

        public async Task<ERecupera> Recupera(ERecupera recupera)
        {
            var user = await _userManager.FindByEmailAsync(recupera.Emial);
            
            if (user != null)
            { 
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var url = ($"https://localhost:7209/ResetPass/code={code}&mail={recupera.Emial}");

                MailCampos mailCampos = new MailCampos();
                string elCuerpo = "<label>Hola !</label> <br /><br />";
                elCuerpo += $"<label>Te escribimos de {Constantes.ElDominio} por que recibimos una solicitud de reinicio de password </label><br />";
                elCuerpo += $"<label>pudes re iniciar tu password ingresando al siguiente enlace:</label><br />";
                elCuerpo += $"<a href={url}>Confirma tu Cuenta</a> <br /><br />";
                elCuerpo += $" En caso de que no lo hayas solicitado tu, por haz caso omiso a este correo <br />";
                elCuerpo += $" ayudanos comunicandote con el administrador del sistema. <br />";

                MailCampos datos = new MailCampos();
                datos = mailCampos.PoblarMail(user.Email, "Recupera tu Password!", elCuerpo,
                    user.Email, "userId", "OrgId", Constantes.DeNombreMail01,
                    Constantes.DeMail01, Constantes.ServerMail01, Constantes.PortMail01,
                    Constantes.UserNameMail01, Constantes.PasswordMail01);

                await _enviarMails.NuevoEmail(datos);
                recupera.IsRecupera = true;
            }
            return recupera;
        }

        public async Task<EAddUsuario> AddUsuario(EAddUsuario addUsuario)
        {
            if (addUsuario.FirmaIn)
            {
                var resultado = await _signInManager.PasswordSignInAsync(
                addUsuario.Mail, addUsuario.Pass, addUsuario.RecordarMe,
                lockoutOnFailure: false);
                addUsuario.Positivo = resultado.Succeeded;
                               
                return addUsuario;
            }
 
            var user = CreateUser();

            await _userStore.SetUserNameAsync(user, addUsuario.Mail, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, addUsuario.Mail, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, addUsuario.Pass);
            
            if (result.Succeeded) 
            {
                var userId = await _userManager.GetUserIdAsync(user);
                await Escribir(user.Id, addUsuario.OrgId,
                    $"Se creo nuevo acceso y password {addUsuario.Mail}", true);
                
                addUsuario.UsuarioId = userId;

                NewUsuario.UsuariosId = userId;
                NewUsuario.Nombre = addUsuario.Mail;
                NewUsuario.Paterno = addUsuario.Mail;
                NewUsuario.Nivel = addUsuario.Nivel;
                NewUsuario.OrgId = addUsuario.OrgId;
                NewUsuario.OldEmail = addUsuario.Mail;
                await _appDbContext.Usuarios.AddAsync(NewUsuario);
                await _appDbContext.SaveChangesAsync();

                await Escribir(user.Id, addUsuario.OrgId,
                    $"Se creo nuevo usuario {addUsuario.Mail}", true);
                
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var url = ($"https://localhost:7209//confirmamail/userId={userId}&code={code}");
                
                MailCampos mailCampos = new MailCampos();
                string elCuerpo = "<label>Hola !</label> <br /><br />";
                elCuerpo += $"<label>Te escribimos de {Constantes.ElDominio} para enviarte un correo de confirmacion de cuenta </label><br />" ;
                elCuerpo += $"<label>por favor confirma tu Cuenta de correo ingresando al siguiente enlace:</label><br />";
                elCuerpo += $"<a href={url}>Confirma tu Cuenta</a> <br />";
                
                MailCampos datos = new MailCampos();
                datos = mailCampos.PoblarMail(addUsuario.Mail, "Confirma Tu correo!", elCuerpo,
                    addUsuario.Mail, userId, NewUsuario.OrgId, Constantes.DeNombreMail01,
                    Constantes.DeMail01, Constantes.ServerMail01, Constantes.PortMail01,
                    Constantes.UserNameMail01, Constantes.PasswordMail01);

                await _enviarMails.NuevoEmail(datos);
            }
            else
            {
                await Escribir(user.Id, addUsuario.OrgId,
                    $"No creo nuevo usuario!!! {addUsuario.Mail}", true);
            }

            return addUsuario;

        }        

        

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
        //[Inject]
        //public I190BitacoraServ BitacoraIServ { get; set; }
        public MyFunc MyFunc { get; set; } = new MyFunc();
        public async Task Escribir(string usuarioId, string ordId,
            string desc, bool sistema)
        {
            Z190_Bitacora bita = new Z190_Bitacora();
            bita = MyFunc.WriteBitacora(usuarioId, ordId, desc, sistema);
            await _bitacoraIServ.AddBitacora(bita);
        }
    }
}

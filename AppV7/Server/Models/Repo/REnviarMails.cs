using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace AppV7.Server.Models.Repo
{
    public class REnviarMails : IEnviarMails 
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly I190Bitacora _bitacoraIServ;

        public REnviarMails(ApplicationDbContext appDbContext,
            I190Bitacora bitacoraIServ)
        {
            _appDbContext = appDbContext;
            _bitacoraIServ = bitacoraIServ;
        }

        public async Task NuevoEmail(MailCampos mailCampos)
        {
            if (mailCampos != null) {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(mailCampos.SenderEmail));
                email.To.Add(MailboxAddress.Parse(mailCampos.Para));
                email.Subject = mailCampos.Titulo;
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = mailCampos.Cuerpo };
                using var smtp = new SmtpClient();
                smtp.Connect(mailCampos.Server, mailCampos.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailCampos.UserName, mailCampos.Password);
                smtp.Send(email);
                smtp.Disconnect(true);

                string texto = $"Se envio email para confirmar nuevo usuario " +
                    $"{mailCampos.Para} en la empresa {mailCampos.OrgId}";
                await Escribir(mailCampos.UserId, mailCampos.OrgId, texto, true);
            }
           
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

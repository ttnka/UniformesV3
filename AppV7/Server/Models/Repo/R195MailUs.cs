using AppV7.Server.Data;
using AppV7.Server.Models.IFace;
using AppV7.Shared;
using Microsoft.EntityFrameworkCore;

namespace AppV7.Server.Models.Repo
{
    public class R195MailUs : I195MailUs
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly IEnviarMails _enviarMails;

        public R195MailUs(ApplicationDbContext appDbContext,
            IEnviarMails enviarMails)
        {
            _appDbContext = appDbContext;
            _enviarMails = enviarMails;
        }

        public async Task<Z195_MailUs> AddMailUs(Z195_MailUs mailUs)
        {
            var res = await _appDbContext.MailUs.AddAsync(mailUs);
            await _appDbContext.SaveChangesAsync();

            SendMails(mailUs);

            return res.Entity;
        }

        public void SendMails(Z195_MailUs mailUsData )
        {
            //Z195_MailUs regMailUs = new();

            string elCuerpo = "<label>Hola !</label> <br /><br />";
            elCuerpo += $"<label>Te escribimos de {Constantes.ElDominio} por que recibimos tu mensaje </label><br />";
            elCuerpo += $"<label>{mailUsData.Desc}</label><br />";
            elCuerpo += $"Estaremos tomando en cuenta y estamos en contacto <br /><br />";
            elCuerpo += $" En caso de que no lo hayas escrito tu esto te pedimos que hagaz caso omiso a este correo <br />";
            elCuerpo += $" ayudanos comunicandote con el administrador del sistema. <br />";

            MailCampos mailCampos = new MailCampos();
            MailCampos datos = new MailCampos();

            datos = mailCampos.PoblarMail(
                para: mailUsData.Email, titulo: "Recibimos tu mensaje!", cuerpo: elCuerpo,
                nombre: Constantes.DeMail01, userId: "userId", orgId: "OrgId", 
                senderName: Constantes.DeNombreMail01, senderEMail:Constantes.DeMail01, 
                server: Constantes.ServerMail01, port: Constantes.PortMail01,
                userName: Constantes.UserNameMail01, password: Constantes.PasswordMail01);

            _enviarMails.NuevoEmail(datos);

            if(!string.IsNullOrEmpty(mailUsData.Para))
            {
                datos.Para = mailUsData.Para;
                datos.Titulo = "Recibimos un nuevo mensaje de contactanos.";
                elCuerpo = "<label>Hola !</label> <br /><br />";
                elCuerpo += $"<label>Recibimos tu mensaje de {mailUsData.Nombre} </label><br />";
                elCuerpo += $"<label>{mailUsData.Desc}</label><br />";
                elCuerpo += $"agrego su Telefono {mailUsData.Cel}<br /><br />";
                elCuerpo += $" Puedes consultar este mensaje el sitio {Constantes.ElDominio} . <br />";

                _enviarMails.NuevoEmail(datos);
            }

        }

        public async Task<IEnumerable<Z195_MailUs>> Buscar(string clave)
        {
            IQueryable<Z195_MailUs> querry = _appDbContext.MailUs;

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
    }
}

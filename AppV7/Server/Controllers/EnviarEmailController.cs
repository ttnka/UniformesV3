using AppV7.Shared;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace AppV7.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnviarEmailController : ControllerBase 
    {
        [HttpPost]
        public IActionResult SendEmail(MailCampos mailCampos)
        {
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
            return Ok();
        }
    }
}

/*
 * var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("info@zuverworks.com"));
            email.To.Add(MailboxAddress.Parse("info@zuverworks.com"));
            email.Subject = "Nuevo Email ZZZ";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };
            using var smtp = new SmtpClient();
            smtp.Connect("mail.omnis.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("info@zuverworks.com", "2468022Ih.");
            smtp.Send(email);
            smtp.Disconnect(true);
            return Ok();
 */

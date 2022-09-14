using AppV7.Shared;

namespace AppV7.Server.Models.IFace
{
    public interface IEnviarMails
    {
        Task NuevoEmail(MailCampos mailCampos); 
    }
}

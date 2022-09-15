using AppV7.Shared;

namespace AppV7.Server.Models.IFace
{
    public interface I195MailUs
    {
        Task<IEnumerable<Z195_MailUs>> Buscar(string clave);
        Task<Z195_MailUs> AddMailUs(Z195_MailUs mailUs);
        //Task<Z195_MailUs> UpDateOrg(Z195_MailUs org);
    }
}

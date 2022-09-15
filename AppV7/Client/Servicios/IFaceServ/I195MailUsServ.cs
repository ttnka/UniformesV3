using AppV7.Shared;

namespace AppV7.Client.Servicios.IFaceServ
{
    public interface I195MailUsServ
    {
        Task<IEnumerable<Z195_MailUs>> Buscar(string clave);
        Task<Z195_MailUs> AddMailUs(Z195_MailUs mailUs);
        //Task<Z195_MailUs> UpDateOrg(Z195_MailUs org);
    }
}

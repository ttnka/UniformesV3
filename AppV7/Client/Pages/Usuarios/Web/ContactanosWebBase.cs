using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using Microsoft.AspNetCore.Components;

namespace AppV7.Client.Pages.Usuarios.Web
{
    public class ContactanosWebBase : ComponentBase 
    {
        public Z195_MailUs MailUs { get; set; } = new();
        public Z840_Contactanos ContactanosConfig { get; set; } = new();
        [Inject]
        public I195MailUsServ MailIServ { get; set; }
        [Inject]
        public I840ContactanosServ ContIServ { get; set; }
        protected async override Task OnInitializedAsync()
        {
            await LeerDatos();
        }
        public async Task LeerDatos()
        {
            var contTemp = await ContIServ.Buscar("General");
            if (contTemp.Any()) ContactanosConfig = contTemp.FirstOrDefault()!;
        }
        public async Task SendMail()
        {
            await MailIServ.AddMailUs(MailUs);
            if (!string.IsNullOrEmpty(ContactanosConfig.ParaMail))
            {

            }
        }
    }
}

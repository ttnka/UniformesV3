using Microsoft.AspNetCore.Components;
using AppV7.Shared;
using AppV7.Client.Servicios.IFaceServ;

namespace AppV7.Client.Pages.Sistema
{
    public class ResetPassBase : ComponentBase 
    {
        [Parameter]
        public string code { get; set; }
        [Parameter]
        public string mail { get; set; }
        public EAddUsuario EaddUsuario { get; set; } = new EAddUsuario();
        [Inject]
        public IAddUsuarioServ AddUserIServ { get; set; }
        protected async override Task OnInitializedAsync()
        {
            EaddUsuario.Mail = mail;
            EaddUsuario.ConfirmacionCode = code;
        }
        /*
        public async Task ResetP()
        {

        }
        */
    }
}

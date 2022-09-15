using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using Microsoft.AspNetCore.Components;

namespace AppV7.Client.Pages.Sistema
{
    public class LogInBase : ComponentBase
    {
        [Inject]
        public IAddUsuarioServ AddUserIServ { get; set; }
        [Inject]
        public NavigationManager NM { get; set; } = null!;
        public EAddUsuario ElNuevo { get; set; } = new();
        [Parameter]
        public string ElMensaje { get; set; } = string.Empty;
        [Parameter]
        public string LaUrl { get; set; } = "/";

        protected async override Task OnInitializedAsync()
        {

            //LaUrl = NM.Uri;
        }
        public async Task FirmaInn()
        {
            ElNuevo.FirmaIn = true;
            var respuesta = await AddUserIServ.AddUsuario(ElNuevo);
            
            LaUrl = respuesta.Positivo ? "/" : "/indexusers";
            //LocalRedirect("/indexuser");
            NM.NavigateTo(LaUrl,true);
        }
    }
}

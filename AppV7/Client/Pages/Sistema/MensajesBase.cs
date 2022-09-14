using Microsoft.AspNetCore.Components;

namespace AppV7.Client.Pages.Sistema
{
    public class MensajesBase : ComponentBase 
    {
        [Parameter]
        public string clave { get; set; } = string.Empty;
        [Inject]
        NavigationManager NM { get; set; }

        public Dictionary<string, string> ClavesDic { get; set; } = new Dictionary<string, string>();

        protected async override Task OnInitializedAsync()
        {
            if (clave == string.Empty) NM.NavigateTo("/");
            await LeerMensajes();
        }

        public async Task LeerMensajes()
        {
            ClavesDic.Add("Recuperar", "Enviamos un Mail a tu cuenta, puedes recuperar tu Password siguiendo las instrucciones");

        }

    }
}

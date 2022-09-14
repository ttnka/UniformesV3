using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace AppV7.Client.Pages.Sistema
{
    public class SistemaDataBase : ComponentBase 
    {
        [Inject]
        public I110UsuariosServ UserIServ { get; set; }
        [Inject]
        public NavigationManager NM { get; set; }
        public Z110_Usuarios ElUsuarioAll { get; set; } = new();
        protected async override Task OnInitializedAsync()
        {
            var autState = await AuthStateTask;
            var user = autState.User;

            if (!user.Identity.IsAuthenticated)
                NM.NavigateTo("/firma?laurl=organizaciones");
            UserIdLogAll = user.FindFirst(c => c.Type == "sub")?.Value!;
            await LeerUsuario();
        }
        public async Task LeerUsuario()
        {
            var ElUserTemp = await UserIServ.Buscar($"UserId_-_UserId_-_{UserIdLogAll}", "Vacio");
            ElUsuarioAll = ElUserTemp!.FirstOrDefault()!;
            if (ElUsuarioAll == null) NM.NavigateTo("/firma?laurl=sistema");
        }
        [CascadingParameter]
        public Task<AuthenticationState> AuthStateTask { get; set; }
        public string UserIdLogAll { get; set; } = string.Empty;

    }
}

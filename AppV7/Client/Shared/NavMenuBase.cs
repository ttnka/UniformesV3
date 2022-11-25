using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace AppV7.Client.Shared
{
    public class NavMenuBase : ComponentBase
    {
        public int ElNivel = 0;
        [Inject]
        public I110UsuariosServ UserIServ { get; set; } = default!;
        protected async override Task OnInitializedAsync()
        {
            var autState = await AuthStateTask;
            var user = autState.User;
            string buscar = string.Empty;
            if (!user.Identity!.IsAuthenticated)
            { ElNivel = 1; }
            else
            {
                var uIdTp = user.FindFirst(c => c.Type == "sub")?.Value;
                buscar = $"UserId_-_UserId_-_{uIdTp}";
                var resultado = await UserIServ.Buscar(buscar, "vacio");
                ElNivel = resultado.FirstOrDefault()!.Nivel;
            }
        }
        [CascadingParameter]
        public Task<AuthenticationState> AuthStateTask { get; set; } = default!;
        public string UserIdLogAll { get; set; } = string.Empty;
        
        public MyFunc MyFunc { get; set; } = new MyFunc();
        

        [Inject]
        public I110UsuariosServ UsuariosIServ { get; set; } = default!;
        [Parameter]
        public Z110_Usuarios ElUsuarioAll { get; set; } = new();
    }
}

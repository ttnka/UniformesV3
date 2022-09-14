using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace AppV7.Client.Pages.Usuarios.MyInfo
{
    
    public class MyInfoAllBase : ComponentBase 
    {
        [CascadingParameter]
        public Task<AuthenticationState> AuthStateTask { get; set; }
        public string UserIdLogAll { get; set; } = string.Empty;
        [Inject]
        public NavigationManager NM { get; set; }
        [Inject]
        public I110UsuariosServ UserIServ { get; set; }
        [Inject]
        public I100OrgServ OrgIServ { get; set; }
        
        public Dictionary<string, string> DatosDicAll { get; set; } =
            new Dictionary<string, string>();
        public Z110_Usuarios ElUsuario { get; set; } = new();
        
        protected async override Task OnInitializedAsync()
        {
            var autState = await AuthStateTask;
            var user = autState.User;
            if (!user.Identity.IsAuthenticated) NM.NavigateTo("/firma?laurl=myinfo"); 
            UserIdLogAll = user.FindFirst(c => c.Type == "sub")?.Value!;
            await LeerUser();
            await LeerDatos();
        }
        public async Task LeerUser()
        {
            var usuarioTemp =
                await UserIServ.Buscar($"UserId_-_UserId_-_{UserIdLogAll}", "vacio");
           

            if (usuarioTemp == null) NM.NavigateTo("/firma?laurl=myinfo");
            ElUsuario = usuarioTemp!.FirstOrDefault()!;
         
        }
        public async Task LeerDatos()
        {
            if (!DatosDicAll.ContainsKey($"Org_{ElUsuario.OrgId}"))
            {
                var laOrg = await OrgIServ.Buscar($"Uno_-_OrgId_-_{ElUsuario.OrgId}");
                if (laOrg != null)
                    DatosDicAll.Add($"Org_{ElUsuario.OrgId}", laOrg.FirstOrDefault()!.RazonSocial!);
               //DatosDicAll.Add($"ElMail_{ElUsuario.UsuariosId}", )
            }
            if (!DatosDicAll.ContainsKey("UsersNivel"))
            {
                string[] titulos = UserNivel.Titulos.Split(",");
                for (int i = 0; i < titulos.Length; i++)
                {
                    DatosDicAll.Add($"Nivel_{i + 1}", titulos[i]);
                }
                DatosDicAll.Add("UsersNivel", "Ok");
            }

        }
       
    }
}

using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared.Libreria;
using AppV7.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen.Blazor;
using System.Security.AccessControl;

namespace AppV7.Client.Pages.Uniformes.Canano
{
    public class BitacoraUnifBase : ComponentBase 
    {
        [Inject]
        public I100OrgServ OrgIServ { get; set; } = default!;
        public IEnumerable<Z110_Usuarios> LosUsers { get; set; } = new List<Z110_Usuarios>();
        public IEnumerable<Z190_Bitacora> LasBitacoras { get; set; } = new List<Z190_Bitacora>();
        public RadzenDataGrid<Z190_Bitacora>? BitacoraGrid { get; set; } = default!;

        protected async override Task OnInitializedAsync()
        {
            await LeerUser();
            await Escribir(ElUsuario.UsuariosId, ElUsuario.OrgId,
                "Consulto la bitacora", false);
            await LeerDatos();
        }

        protected async Task LeerDatos()
        {
            string buscar = "Comerciante";
            if (ElUsuario.Nivel > 3)
                buscar = "All";
            LasBitacoras = await BitacoraIServ.Buscar(buscar, ElUsuario.UsuariosId);
        }

        [CascadingParameter]
        public Task<AuthenticationState> AuthStateTask { get; set; } = default!;
        public string UserIdLogAll { get; set; } = string.Empty;

        [Inject]
        public I190BitacoraServ BitacoraIServ { get; set; } = default!;
        public MyFunc MyFunc { get; set; } = new MyFunc();
        public async Task Escribir(string usuarioId, string ordId,
            string desc, bool sistema)
        {
            Z190_Bitacora bita = new();
            bita = MyFunc.WriteBitacora(usuarioId, ordId, desc, sistema);
            await BitacoraIServ.AddBitacora(bita);
        }

        [Inject]
        public I110UsuariosServ UsersIServ { get; set; } = default!;
        [Parameter]
        public Z110_Usuarios ElUsuario { get; set; } = new();
        [Inject]
        public NavigationManager NM { get; set; } = default!;
        public async Task LeerUser()
        {
            var autState = await AuthStateTask;
            var user = autState.User;
            if (!user.Identity!.IsAuthenticated) NM.NavigateTo("/firma?laurl=/inicio");
            UserIdLogAll = user.FindFirst(c => c.Type == "sub")?.Value!;
            LosUsers = await UsersIServ.Buscar("All", "Vacio");
            ElUsuario = LosUsers.FirstOrDefault(x => x.UsuariosId == UserIdLogAll)!;
            /*
            LosUsers = await UserIServ.Buscar($"UserId_-_UserId_-_{UserIdLogAll}", "vacio");
            ElUsuario = UserList.FirstOrDefault()!;
            */
        }
    }
}

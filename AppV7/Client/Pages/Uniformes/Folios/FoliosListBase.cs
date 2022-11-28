using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared.Libreria;
using AppV7.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using Radzen.Blazor;

namespace AppV7.Client.Pages.Uniformes.Folios
{
    public class FoliosListBase : ComponentBase 
    {
        [Inject]
        public I260FolioServ FolioIServ { get; set; } = default!;
        public IEnumerable<Z260_Folio> LosFolios { get; set; } = Enumerable.Empty<Z260_Folio>();
        public bool Editando { get; set; } = false;
        public RadzenDataGrid<Z260_Folio>? FolioGrid { get; set; } = default!;
        public List<string> FileNameList { get; set; } = new List<string>();
        protected override async Task OnInitializedAsync()
        {
            await LeerUser();
            await LeerDatos();
        }
        protected async Task LeerDatos()
        {
            LosFolios = await FolioIServ.Buscar("Alla");
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
        public NotificationMessage ElMsn(string tipo, string titulo, string mensaje, int duracion)
        {
            NotificationMessage respuesta = new();
            switch (tipo.ToLower())
            {
                case "info":
                    respuesta.Severity = NotificationSeverity.Info;
                    break;
                case "error":
                    respuesta.Severity = NotificationSeverity.Error;
                    break;
                case "warning":
                    respuesta.Severity = NotificationSeverity.Warning;
                    break;
                default:
                    respuesta.Severity = NotificationSeverity.Success;
                    break;
            }
            respuesta.Summary = titulo;
            respuesta.Detail = mensaje;
            respuesta.Duration = 4000 + duracion;
            return respuesta;
        }

        [Inject]
        public I110UsuariosServ UsersIServ { get; set; } = default!;
        [Parameter]
        public Z110_Usuarios ElUsuario { get; set; } = new();
        public IEnumerable<Z110_Usuarios> LosUsers { get; set; } =
            Enumerable.Empty<Z110_Usuarios>();
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
            var UserList = await UserIServ.Buscar($"UserId_-_UserId_-_{UserIdLogAll}", "vacio");
            ElUsuario = UserList.FirstOrDefault()!;
            */
        }
    }
}

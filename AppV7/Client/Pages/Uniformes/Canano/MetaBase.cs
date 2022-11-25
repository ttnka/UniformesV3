using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared.Libreria;
using AppV7.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using Radzen.Blazor;

namespace AppV7.Client.Pages.Uniformes.Canano
{
    public class MetaBase : ComponentBase 
    {
        [Inject]
        public I290MetaServ MetaIServ { get; set; } = default!;
        public IEnumerable<Z290_Meta> LasMetas { get; set; } = new List<Z290_Meta>();
        public List<string> LosTipos { get; set; } = new List<string>();
        public List<string> LosMpios { get; set; } = new List<string>();
        public bool Editando { get; set; } = false;
        public RadzenDataGrid<Z290_Meta>? MetaGrid { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            await LeerUser();
            LeerTipos();
            await Escribir(ElUsuario.UsuariosId, ElUsuario.OrgId,
                            "Consulta del listado de metas", false);
        }
        public void LeerTipos()
        {
            string[] tiposTemp = Constantes.MetasTipo.Split(",");
            foreach (var tipo in tiposTemp)
            {
                LosTipos.Add(tipo);
            }
            string[] NomMpios = Constantes.MpiosTodos.Split(",");
            for (int i = 0; i < NomMpios.Length; i++)
            {
                LosMpios.Add(NomMpios[i]);
            }

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
        public I110UsuariosServ UserIServ { get; set; } = default!;
        [Parameter]
        public Z110_Usuarios ElUsuario { get; set; } = new();
        public IEnumerable<Z110_Usuarios> LosUsers { get; set; } = 
            Enumerable.Empty<Z110_Usuarios>();
        public NavigationManager NM { get; set; } = default!;
        public async Task LeerUser()
        {
            var autState = await AuthStateTask;
            var user = autState.User;
            if (!user.Identity!.IsAuthenticated) NM.NavigateTo("/firma?laurl=/inicio");
            UserIdLogAll = user.FindFirst(c => c.Type == "sub")?.Value!;

            LosUsers = await UserIServ.Buscar("Alla", "Vacio");
            ElUsuario = LosUsers.FirstOrDefault(x => x.UsuariosId == UserIdLogAll)!;

            /*
            var UserList = await UserIServ.Buscar($"UserId_-_UserId_-_{UserIdLogAll}", "vacio");
            ElUsuario = UserList.FirstOrDefault()!;
            */
        }

    }
}

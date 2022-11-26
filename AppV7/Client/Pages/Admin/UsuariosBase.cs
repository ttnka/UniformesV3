using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using Radzen.Blazor;

namespace AppV7.Client.Pages.Admin
{
    public class UsuariosBase : ComponentBase
    {
        [Inject]
        public I100OrgServ OrgIServ { get; set; } = default!;
        public IEnumerable<Z110_Usuarios> LosUsers { get; set; } =
            new List<Z110_Usuarios>();
        public IEnumerable<Z100_Org> LasOrgs { get; set; } =
                Enumerable.Empty<Z100_Org>();
        public List<KeyValuePair<int, string>> LosNiveles { get; set; } =
            new List<KeyValuePair<int, string>>();
        public List<string> LosMpios { get; set; } = new List<string>();
        [Parameter]
        public Dictionary<string, string> DatosDic { get; set; } =
            new Dictionary<string, string>();

        public bool Editando { get; set; } = false;

        public RadzenDataGrid<Z110_Usuarios>? UsersGrid = default!;
        protected async override Task OnInitializedAsync()
        {
            await LeerUser();
            LeerMunicipios();
            await LeerOrgs();
        }

        public async Task LeerUsers()
        {
            LosUsers = await UsersIServ.Buscar("Allo", "Vacio");
        }

        protected void LeerMunicipios()
        {
            string[] NomMpios = Constantes.MpiosTodos.Split(",");
            for (int i = 0; i < NomMpios.Length; i++)
            {
                LosMpios.Add(NomMpios[i]);
            }
        }
        public async Task LeerOrgs()
        {
            LasOrgs = await OrgIServ.Buscar("All");
            if (LasOrgs != null)
            {
                foreach (var orgt in LasOrgs)
                {
                    if (!DatosDic.ContainsKey($"Org_{orgt.OrgId}"))
                        DatosDic.Add($"Org_{orgt.OrgId}", orgt.Comercial);
                }
            }
            if (!DatosDic.ContainsKey("UsersNivel"))
            {
                string[] titulos = UserNivel.Titulos.Split(",");
                for (int i = 0; i < titulos.Length; i++)
                {
                    if (ElUsuario.Nivel > i + 1)
                        LosNiveles.Add(new KeyValuePair<int, string>(i + 1, titulos[i]));
                    DatosDic.Add($"Nivel_{i + 1}", titulos[i]);
                }
                DatosDic.Add("UsersNivel", "Ok");
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
            var bita = MyFunc.WriteBitacora(usuarioId, ordId, desc, sistema);
            await BitacoraIServ.AddBitacora(bita);
        }

        public NotificationMessage ElMsn(string tipo, string titulo, string mensaje, int duracion )
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
        [Inject]
        public NavigationManager NM { get; set; } = default!;
        public async Task LeerUser()
        {
            var autState = await AuthStateTask;
            var user = autState.User;
            if (!user.Identity!.IsAuthenticated) NM.NavigateTo("/firma?laurl=/inicio");
            UserIdLogAll = user.FindFirst(c => c.Type == "sub")?.Value!;
            
            LosUsers = await UsersIServ.Buscar("Allo", "Vacio");
            ElUsuario = LosUsers.FirstOrDefault(x => x.UsuariosId == UserIdLogAll)!;
            
            /*
            var UserList = await UsersIServ.Buscar($"UserId_-_UserId_-_{UserIdLogAll}", "vacio");
            ElUsuario = UserList.FirstOrDefault()!;
            */
        }
    }
}

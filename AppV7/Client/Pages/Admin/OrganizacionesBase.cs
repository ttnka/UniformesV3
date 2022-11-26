using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using Radzen.Blazor;
using System.Diagnostics;

namespace AppV7.Client.Pages.Admin
{
    public class OrganizacionesBase : ComponentBase 
    {
        [Inject]
        public I100OrgServ OrgIServ { get; set; } = default!;
        public IEnumerable<Z100_Org> LasOrgs { get; set; } = 
            Enumerable.Empty<Z100_Org>();
        

        [Parameter]
        public Dictionary<string, string> OrgDic { get; set; } = new(); 
        
        public bool Editando { get; set; } = false;
        public bool HayWebMaster { get; set; } = false;
        public RadzenDataGrid<Z100_Org>? OrgGrid { get; set; } = default!;
        protected async override Task OnInitializedAsync()
        {
            await LeerUser();
            await LeerDatos();
            await Escribir(ElUsuario.UsuariosId, ElUsuario.OrgId,
                            "Consulta de las Organizaciones desde sistema", false);
        }
        protected async Task LeerDatos()
        {
            LasOrgs = await OrgIServ.Buscar("Allo");
            foreach (var org in LasOrgs)
            {
                if (org.WebAdmin)
                    HayWebMaster = true; 
                if (!OrgDic.ContainsKey(org.Rfc))
                    OrgDic.Add(org.Rfc, org.OrgId);
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
            /*
            LosUsers = await UsersIServ.Buscar("Allo", "Vacio");
            ElUsuario = LosUsers.FirstOrDefault(x => x.UsuariosId == UserIdLogAll)!;
            */
            
            var UserList = await UsersIServ.Buscar($"UserId_-_UserId_-_{UserIdLogAll}", "vacio");
            ElUsuario = UserList.FirstOrDefault()!;
            
        }

    }
}

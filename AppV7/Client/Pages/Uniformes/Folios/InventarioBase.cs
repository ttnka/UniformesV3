using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared.Libreria;
using AppV7.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using Radzen.Blazor;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;

namespace AppV7.Client.Pages.Uniformes.Folios
{
    public class InventarioBase : ComponentBase
    {
        [Inject]
        public I230SolicitudServ SolIServ { get; set; } = default!;
        [Inject]
        public I232DetSolServ DetIServ { get; set; } = default!;
        [Inject]
        public I210AlmacenServ AlmIServ { get; set; } = default!;
        public IEnumerable<Z210_Almacen> LosAlmacenes { get; set; } = 
            Enumerable.Empty<Z210_Almacen>();
        public List<string> LosMpios { get; set; } = new();
        public List<string> LosTipos { get; set; } = new();
        public List<Z230_Solicitud> LasSolicitudes { get; set; } = 
            new List<Z230_Solicitud>();
        public IEnumerable<Z232_DetSol> LosDet { get; set; } = 
            new List<Z232_DetSol>();
        public Filtro ElFiltro { get; set; } = new();
        public bool Datos { get; set; } = false;
        public RadzenTemplateForm<Filtro>? FiltroTemp { get; set; } = default!;
        public RadzenDataGrid<Z230_Solicitud>? SolGrid { get; set; } = default!;
        protected async override Task OnInitializedAsync()
        {   
            await LeerUsers();
            await LeerAlmacenes();
            LeerTipos();
            LeerMunicipios();
            await LeerSolicitudes(ElFiltro.AlmacenF, ElFiltro.ComercioF, 
                ElFiltro.TipoF);
            
        }
        public async Task LeerUsers()
        {
            LosUsers = await UsersIServ.Buscar("Afiliados", "vacio");
            LosDet = await DetIServ.Buscar("Alla");
        }
        public void LeerTipos()
        {
            string[] tiposTemp = Constantes.SolicitudTipo.Split(",");
            foreach (var tipo in tiposTemp)
            {
                LosTipos.Add(tipo);
            }
        }
        public async Task LeerAlmacenes()
        {
            LosAlmacenes = await AlmIServ.Buscar("Alla");
        }
        public class Filtro
        {
            public string AlmacenF { get; set; } = "Alla";
            public string ComercioF { get; set; } = "Alla";
            public string TipoF { get; set; } = "Alla";
            public bool Filtrado { get; set; } = false;
        }
        public async Task LeerSolicitudes(string alma, string comer, string tipo)
        {
            var temp = await SolIServ.Buscar("Alla");
            LasSolicitudes = temp.ToList();
            Datos = true;
            /*
            LasSolicitudes = temp.Where(x =>
                (alma == "Alla" ? x.Almacen != null : x.Almacen.Equals(comer)) &
                
                (tipo == "Alla" ? x.Tipo != null : x.Tipo == tipo)).ToList() ;
            //(comer == "Alla" ? x.Usuario != null : x.Usuario == comer) &
            */
        }
        protected void LeerMunicipios()
        {
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
            var UserList = await UsersIServ.Buscar($"UserId_-_UserId_-_{UserIdLogAll}", "vacio");
            ElUsuario = UserList.FirstOrDefault()!;

        }

    }
}

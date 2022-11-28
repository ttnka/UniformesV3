using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared.Libreria;
using AppV7.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen.Blazor;
using Radzen;

namespace AppV7.Client.Pages.Uniformes.Canano
{
    public class DetSolBase : ComponentBase 
    {
        [Inject]
        public I232DetSolServ DetIServ { get; set; } = default!;
        public IEnumerable<Z232_DetSol> LosDets { get; set; } = 
            Enumerable.Empty<Z232_DetSol>();
        [Inject]
        public I220ProductoServ ProductosIServ { get; set; } = default!;
        public IEnumerable<Z220_Producto> LosProductos { get; set; } = 
            Enumerable.Empty<Z220_Producto>();
        public List<string> LosGpos { get; set; } = new();
        public List<string> LaRopaTallas { get; set; } = new();
        public List<string> LosZapTallas { get; set; } = new();
        public bool Editando { get; set; }  = false;
        public bool Editando2 { get; set; } = false;
        public bool ShowDesc { get; set; } = false;
        public Dictionary<string, string> DataDetDic { get; set; } = new();
        public NavigationManager NM { get; set; } = default!;
        public RadzenDataGrid<Z232_DetSol>? DetGrid { get; set; } = default!;
         
        protected async override Task OnInitializedAsync()
        {
            await LeerProductos();
            await LeerDatos();
            LeerCerrados();
        }

        protected void LeerCerrados()
        {
            if(ElFolioDet.Estado == 1 || ElFolioDet.Estado == 4)
            {
                Editando = true;
                Editando2 = true;
            }
        }

        protected async Task LeerDatos()
        {
            LosDets = await DetIServ.Buscar($"Folio_-_Folio_-_{ElFolioDet.Folio}");
        }
        protected async Task LeerProductos()
        {
            LosProductos = await ProductosIServ.Buscar("Alla");
        }
        protected void LeerGpoTallas()
        {
            string[] gpoTemp = Constantes.Grupos.Split(",");
            foreach (var gpo in gpoTemp)
            {
                LosGpos.Add(gpo);
            }
            string[] ztallaTemp = Constantes.ZapatoTallas.Split(",");
            foreach (var ztalla in ztallaTemp)
            {
                LosZapTallas.Add(ztalla);
            }
            string[] rTallasTemp = Constantes.RopaTallas.Split(",");
            foreach (var rTallas in rTallasTemp)
            {
                LaRopaTallas.Add(rTallas);
            }
        }
        public async Task ActualizarSol(string solId)
        {

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
        public I110UsuariosServ UsuariosIServ { get; set; } = default!;
        [Parameter]
        public Z110_Usuarios ElUsuarioDet { get; set; } = new();
        [Parameter]
        public Z230_Solicitud ElFolioDet { get; set; } = default!;  
    }
}

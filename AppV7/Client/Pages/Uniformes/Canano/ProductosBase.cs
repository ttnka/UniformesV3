using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared.Libreria;
using AppV7.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen.Blazor;
using Radzen;

namespace AppV7.Client.Pages.Uniformes.Canano
{
    public class ProductosBase : ComponentBase 
    {
        [Inject]
        public I220ProductoServ ProductoIServ { get; set; } = default!;
        public IEnumerable<Z220_Producto> LosProductos { get; set; } = 
            Enumerable.Empty<Z220_Producto>();
        public List<string> LosZapTallas { get; set; } = new List<string>();
        public List<string> LaRopaTallas { get; set; } = new List<string>();
        public List<string> LosGpos { get; set; } = new List<string>();
        public bool Editando { get; set; } = false;
        public Dictionary<string, Z220_Producto> ProdDic { get; set; } = new();
        public RadzenDataGrid<Z220_Producto>? ProdGrid { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            await LeerUser();
            await LeerDatos();
            LeerGpoTallas();
            await Escribir(ElUsuario.UsuariosId, ElUsuario.OrgId,
                            "Consulta de lista de productos", false);
        }
        protected async Task LeerDatos()
        {
            LosProductos = await ProductoIServ.Buscar("Alla");
        }
        protected void LeerGpoTallas()
        {
            string[] gpoTemp = Constantes.Grupos.Split(",");
            foreach(var gpo in gpoTemp)
            {
                LosGpos.Add(gpo);
            }
            string[] ztallaTemp = Constantes.ZapatoTallas.Split(",");
            foreach(var ztalla in ztallaTemp)
            {
                LosZapTallas.Add(ztalla);
            }
            string[] rTallasTemp = Constantes.RopaTallas.Split(",");
            foreach(var rTallas in rTallasTemp)
            {
                LaRopaTallas.Add(rTallas);
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
            LosUsers = await UserIServ.Buscar("Allo", "Vacio");
            ElUsuario = LosUsers.FirstOrDefault(x => x.UsuariosId == UserIdLogAll)!;
            */
            
            var UserList = await UsersIServ.Buscar($"UserId_-_UserId_-_{UserIdLogAll}", "vacio");
            ElUsuario = UserList.FirstOrDefault()!;
            
        }

    }
}

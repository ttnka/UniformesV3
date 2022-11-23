using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared.Libreria;
using AppV7.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen.Blazor;

namespace AppV7.Client.Pages.Uniformes.Canano
{
    public class ProductosBase : ComponentBase 
    {
        [Inject]
        public I110UsuariosServ UserIServ { get; set; } = default!;
        [Inject]
        public I220ProductoServ ProductoIServ { get; set; } = default!;
        public IEnumerable<Z220_Producto> LosProductos { get; set; } = 
            Enumerable.Empty<Z220_Producto>();
        public List<string> LosZapTallas { get; set; } = new List<string>();
        public List<string> LaRopaTallas { get; set; } = new List<string>();
        public List<string> LosGpos { get; set; } = new List<string>();
        public bool Editando { get; set; } = false;
        public Dictionary<string, Z220_Producto> ProdDic { get; set; } = new();
        public NavigationManager NM { get; set; } = default!;
        public RadzenDataGrid<Z220_Producto>? ProdGrid { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            var autState = await AuthStateTask;
            var user = autState.User;
            if (!user.Identity.IsAuthenticated) NM.NavigateTo("/firma?laurl=/inicio");
            UserIdLogAll = user.FindFirst(c => c.Type == "sub")?.Value!;
            var UserList = await UserIServ.Buscar($"UserId_-_UserId_-_{UserIdLogAll}", "vacio");
            ElUsuario = UserList.FirstOrDefault()!;
            //LeerMunicipios();
            await LeerDatos();
            LeerGpoTallas();
            await Escribir(ElUsuario.UsuariosId, ElUsuario.OrgId,
                            "Consulta de lista de productos", false);
        }

        protected async Task LeerDatos()
        {
            LosProductos = await ProductoIServ.Buscar("Alla");
            /*
            if (LosProductos.Any())
            {
                foreach(var prod in LosProductos)
                {
                    if (!ProdDic.ContainsKey(prod.Corto)) ProdDic.Add(prod.Corto, prod);
                }
            }
            */
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

        [Inject]
        public I110UsuariosServ UsuariosIServ { get; set; } = default!;
        [Parameter]
        public Z110_Usuarios ElUsuario { get; set; } = new();
    }
}

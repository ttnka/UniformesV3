using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace AppV7.Client.Pages.Zuver.WebConfiguracion
{
    public class WebConfigBase : ComponentBase
    {
        [Inject]
        public I800WebSiteServ WebSiteIServ { get; set; }
        public IEnumerable<Z800_WebSite> LasWSites { get; set; } =
            Enumerable.Empty<Z800_WebSite>();
        [Inject]
        public NavigationManager NM { get; set; } = default!;
        public RadzenDataGrid<Z800_WebSite>? WSiteGrid { get; set; } = default!;
        public bool Editando = false;
        protected override async void OnInitialized()
        {
            await LeerDatos();
        }
        public async Task LeerDatos()
        {
            LasWSites = await WebSiteIServ.Buscar("Allo");

        }

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

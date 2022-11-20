using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace AppV7.Client.Pages.LaWeb
{
    public class LaWebShowBase : ComponentBase 
    {
        [Inject]
        public I800WebSiteServ WebSiteIServ { get; set; } = default!;
        [Inject]
        public I110UsuariosServ UsuarioIServ { get; set; } = default!; 
        public IEnumerable<Z800_WebSite> WsAll =
            new List<Z800_WebSite>();
        public List<Z800_WebSite> WsMenu = new();
        public Dictionary<string, string> DatosDicAll { get; set; } =
            new Dictionary<string, string>();
        public IDictionary<string, object> Parametros =
            new Dictionary<string, object>();
        public int SelectedIndex { get; set; } = 0;

        protected async override Task OnInitializedAsync()
        {
            var autState = await AuthStateTask;
            var user = autState.User;
            string buscar = string.Empty;
            if (!user.Identity.IsAuthenticated)
            {
                var uIdTp = user.FindFirst(c => c.Type == "sub")?.Value;
                buscar = $"UserId_-_UserId_-_{uIdTp}";
            } 
            else
            {
                buscar = $"OldEmail_-_OldEmail_-_{Constantes.DeMailPublico}";
            }
            ElUsuarioAll = (await UsuarioIServ.Buscar(buscar, "vacio")).FirstOrDefault();
            //  NM.NavigateTo("/firma?laurl=myinfo");
            UserIdLogAll = user.FindFirst(c => c.Type == "sub")?.Value!;
            await LeerDatosWebSites();

            SIndex();

        }
        public void SIndex()
        {
            var valor = DateTime.Now.Second;
            for (int i = 0; i < 15; i++)
            {
                if (valor > 4)
                { valor -= 5; }
                else { i = 20; }
            }
            SelectedIndex = valor;
        }

        public async Task LeerDatosWebSites()
        {
            WsAll = await WebSiteIServ.Buscar("Allo");
            if (WsAll.Count() < 1) return;
            foreach (var lws in WsAll)
            {
                if (lws.Status && lws.Estado == 1 &&
                    !string.IsNullOrEmpty(lws.Ceja)) WsMenu.Add(lws);

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
        public Z110_Usuarios ElUsuarioAll { get; set; } = new();
    }
}

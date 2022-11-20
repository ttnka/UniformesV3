using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using Microsoft.AspNetCore.Components;

namespace AppV7.Client.Pages.Usuarios.Web
{
    public class WebAllBase : ComponentBase
    {
        [Inject]
        public I800WebSiteServ WebSiteIServ { get; set; }
        public IEnumerable<Z800_WebSite> WsAll =
            new List<Z800_WebSite>();
        public List<Z800_WebSite> WsMenu = new();
        public Dictionary<string, string> DatosDicAll { get; set; } =
            new Dictionary<string, string>();
        public IDictionary<string, object> Parametros =
            new Dictionary<string, object>();
        public int selectedIndex { get; set; } = 0;

        protected async override Task OnInitializedAsync()
        {
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
            selectedIndex = valor;
        }

        public async Task LeerDatosWebSites()
        {
            WsAll = await WebSiteIServ.Buscar("Allo");
            foreach (var lws in WsAll)
            {
                if (lws.Status && lws.Estado == 1 &&
                    !string.IsNullOrEmpty(lws.Ceja)) WsMenu.Add(lws);

            }

        }
    }
}

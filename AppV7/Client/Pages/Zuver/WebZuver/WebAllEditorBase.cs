using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace AppV7.Client.Pages.Zuver.WebZuver
{
    public class WebAllEditorBase : ComponentBase
    {
        public Z110_Usuarios ElUsuarioAll { get; set; } = new();
        public IEnumerable<Z800_WebSite> LosWSAll { get; set; } =
            new List<Z800_WebSite>();
        public IEnumerable<Z800_WebSite> LosCompAll { get; set; } = 
            new List<Z800_WebSite>();
        
        [Inject]
        public I110UsuariosServ UserIServ { get; set; }
        [Inject]
        public I800WebSiteServ WebSiteIServ { get; set; }
        [Inject]
        public NavigationManager NM { get; set; }
        public Dictionary<string, string> DatosDicAll { get; set; } =
            new Dictionary<string, string>();
        public IDictionary<string, object> Parametros = 
            new Dictionary<string, object>();
        protected async override Task OnInitializedAsync()
        {
            var autState = await AuthStateTask;
            var user = autState.User;
            if (!user.Identity.IsAuthenticated)
                NM.NavigateTo("/firma?laurl=webeditor");
            UserIdLogAll = user.FindFirst(c => c.Type == "sub")?.Value!;
            await LeerUsuario();
            await Raiz();
            await LeerDatosContactanos();

            await LoadParametros();
        }
        public async Task LeerDatosContactanos()
        {
            var tWs = await WebSiteIServ.Buscar("Allo");
            List<Z800_WebSite> WsAll = new();
            List<Z800_WebSite> WsAct = new();
            foreach (var lws in tWs)
            {
                if (lws.Nivel != 0) WsAll.Add(lws);
                if (lws.Status) WsAct.Add(lws);
                if (lws.Nivel == 1 && lws.Estado == 1)
                {
                    if (!DatosDicAll.ContainsKey($"Componente_{lws.Componente}"))
                        DatosDicAll.Add($"Componente_{lws.Componente}", lws.Catalogo);
                }
            }
            LosWSAll = WsAll;
            LosCompAll = WsAct;       
        }
        public async Task Raiz()
        {
            var raiz = await WebSiteIServ.Buscar("Raiz");
            if (raiz.Count() == 0)
            {
                Z800_WebSite nWS = new();
                nWS.Catalogo = "10-10-10-10";
                nWS.Nivel = 0;
                nWS.Indice = 0;
                nWS.Titulo = "Raiz";
                
                await WebSiteIServ.AddWebSite(nWS);
            }
        }
        public async Task LeerUsuario()
        {
            var ElUserTemp = await UserIServ.Buscar($"UserId_-_UserId_-_{UserIdLogAll}", "Vacio");
            ElUsuarioAll = ElUserTemp!.FirstOrDefault()!;
            if (ElUsuarioAll == null) NM.NavigateTo("/firma?laurl=sistema");
        }
        public async Task LoadParametros()
        {
            
        }

        [CascadingParameter]
        public Task<AuthenticationState> AuthStateTask { get; set; }
        public string UserIdLogAll { get; set; } = string.Empty;

        [Inject]
        public I190BitacoraServ BitacoraIServ { get; set; }
        public MyFunc MyFunc { get; set; } = new MyFunc();
        public async Task Escribir(string usuarioId, string ordId,
            string desc, bool sistema)
        {
            var bita = MyFunc.WriteBitacora(usuarioId, ordId, desc, sistema);
            await BitacoraIServ.AddBitacora(bita);
        }

    }
}

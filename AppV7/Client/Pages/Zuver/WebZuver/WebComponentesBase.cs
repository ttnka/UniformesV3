using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace AppV7.Client.Pages.Zuver.WebZuver
{
    public class WebComponentesBase : ComponentBase 
    {
        [Inject]
        public I800WebSiteServ WebSiteIServ { get; set; }
        
        public IEnumerable<Z800_WebSite> LosWS { get; set; } = 
            new List<Z800_WebSite>();
        public IEnumerable<Z800_WebSite> LosCats { get; set; } =
            new List<Z800_WebSite>();
        public List<KeyValuePair<string, string>> LosComp { get; set; } =
            new List<KeyValuePair<string, string>>();
        public List<KeyValuePair<string, string>> LasWebs { get; set; } =
            new List<KeyValuePair<string, string>>();
        public List<KeyValuePair<string, string>> LaCapt { get; set; } =
            new List<KeyValuePair<string, string>>();
        public Z800_WebSite ElWebSite { get; set; } = new();
        public RadzenDataGrid<Z800_WebSite> WSGrid { get; set; } =
            new RadzenDataGrid<Z800_WebSite>();
        public bool Editando { get; set; } = false;
        [Parameter]
        public Z110_Usuarios ElUsuario { get; set; } = null!;
        [Inject]
        public NavigationManager NM { get; set; }
        public Dictionary<string, string> DatosDic { get; set; } = 
            new Dictionary<string, string>();
        protected override async Task OnInitializedAsync()
        {
            if (ElUsuario == null) NM.NavigateTo("/firma?laUrl=webeditor");
            await LeerWebSite();
        }

        public async Task LeerWebSite()
        {
            var tWs = await WebSiteIServ.Buscar("Allo");
            List<Z800_WebSite> WsAll = new ();
            List<Z800_WebSite> WsAct = new();
            foreach (var lws in tWs)
            {
                if (lws.Nivel != 0) WsAll.Add(lws);
                if (lws.Status) WsAct.Add(lws);
//Lee los registros
                if (!DatosDic.ContainsKey($"CatNiv_{lws.Catalogo}"))
                        DatosDic.Add($"CatNiv_{lws.Catalogo}", lws.Nivel.ToString());
           
            }
            LosWS = WsAll;
            LosCats = WsAct;

//  Lee los componentes y los ponen en una lista
            var losComponentes = MyFunc.Componentes();
            foreach (var lcs in losComponentes)
            {
                if (LosComp == null || !LosComp.Any(x => x.Key == lcs)) 
                LosComp!.Add(new KeyValuePair<string, string>(lcs, lcs));
            }
            var lasWebsTemp = MyFunc.WebSites();
            foreach (var lws in lasWebsTemp)
            {
                if (LasWebs == null || !LasWebs.Any(x => x.Key == lws))
                    LasWebs!.Add(new KeyValuePair<string, string>(lws, lws));
            }
            var laCaptTemp = MyFunc.Captura();
            foreach (var lct in laCaptTemp)
            {
                if (LaCapt == null || !LaCapt.Any(x => x.Key == lct))
                    LaCapt!.Add(new KeyValuePair<string, string>(lct, lct));
            }
        }
        
        public string SigCat(string cat, int niv)
        {
            string resultado = "00";
            string[] array = cat.Split("-");
            int oldV = int.Parse(array[niv]);
            for (var i=1; i < 100; i++)
            {
                string newCat = string.Empty;
                for(var j=0; j < array.Length ; j++)
                {
                    newCat += j == niv ? 
                        (oldV + i).ToString() : array[j];
                    newCat += j <= 2 ? "-" : "";
                }

                if (!DatosDic.ContainsKey($"CatNiv_{newCat}"))
                {
                    DatosDic.Add($"CatNiv_{newCat}", (niv + 1).ToString());
                    return newCat;
                }
            }

            return resultado;
        }

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

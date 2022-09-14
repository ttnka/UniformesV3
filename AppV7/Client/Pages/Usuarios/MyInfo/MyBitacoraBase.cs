using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace AppV7.Client.Pages.Usuarios.MyInfo
{
    public class MyBitacoraBase : ComponentBase 
    {
        [Parameter]
        public Z110_Usuarios ElUsuario { get; set; } = new Z110_Usuarios();
        [Parameter]
        public Dictionary<string, string> DataDic { get; set; } = 
            new Dictionary<string, string>();
        [Inject]
        public I100OrgServ OrgsIServ { get; set; }
        [Inject]
        public I110UsuariosServ UsersIServ { get; set; }
        [Inject]
        public I190BitacoraServ BitaIServ { get; set; }
        public IEnumerable<Z190_Bitacora> LasBits { get; set; } = 
            new List<Z190_Bitacora>();
        
        public RadzenDataGrid<Z190_Bitacora> BitaGrid { get; set; } =
           new RadzenDataGrid<Z190_Bitacora>();
        public bool ElSystema { get; set; } = false;
        protected async override Task OnInitializedAsync()
        {
            await LeerBitacoras(ElSystema);
            await LeerOrgs();
            await LeerNombres();
        }

        public async Task LeerBitacoras(bool sistema)
        {
            string filtro = sistema ? "Sistema" : "OrgX";
            LasBits = await BitaIServ.Buscar(filtro, ElUsuario.OrgId);
        }
        public async Task LeerOrgs()
        {
            var orgsTmp = await OrgsIServ.Buscar($"Uno_-_OrgId_-_{ElUsuario.OrgId}");
            foreach (var orgs in orgsTmp)
            {
                if (!DataDic.ContainsKey($"Org_{orgs.OrgId}"))
                    DataDic.Add($"Org_{orgs.OrgId}", orgs.Comercial);
            }
        }
        public async Task LeerNombres()
        {
            var nomTmp = await UsersIServ.Buscar("Org", ElUsuario.OrgId);
            foreach (var nom in nomTmp) {
                if (!DataDic.ContainsKey($"Nombre_{nom.UsuariosId}"))
                    DataDic.Add($"Nombre_{nom.UsuariosId}",
                        $"{nom.Nombre} {nom.Paterno}");
            }
        }
        public MyFunc MyFunc { get; set; } = new();
    }
}

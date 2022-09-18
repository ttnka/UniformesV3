using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen.Blazor;

namespace AppV7.Client.Pages.Admin
{
    public class OrganizacionesBase : ComponentBase 
    {
        [Inject]
        public I100OrgServ OrgIServ { get; set; }
        public IEnumerable<Z100_Org> LasOrgs { get; set; } = 
            Enumerable.Empty<Z100_Org>();
        public RadzenDataGrid<Z100_Org> OrgGrid { get; set; } = 
            new RadzenDataGrid<Z100_Org>();
        [Parameter]
        public Dictionary<string, string> OrgDic { get; set; } = new(); 
        public bool isLoading { get; set; } = false;
        public bool Editando { get; set; } = false;
        public bool HayWebMaster { get; set; } = false;
        [Inject]
        public NavigationManager NM { get; set; }
        protected async override Task OnInitializedAsync()
        {
            await LeerDatos();
            await Escribir(ElUsuario.UsuariosId, ElUsuario.OrgId,
                            "Consulta de las Organizaciones desde sistema", false);
        }
        
        protected async Task LeerDatos()
        {
            isLoading = true;
            LasOrgs = await OrgIServ.Buscar("All");
            foreach (var org in LasOrgs)
            {
                if (org.WebAdmin)
                    HayWebMaster = true; 
                if (!OrgDic.ContainsKey(org.Rfc))
                    OrgDic.Add(org.Rfc, org.OrgId);
            }
            isLoading = false;
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
            Z190_Bitacora bita = new Z190_Bitacora();
            bita = MyFunc.WriteBitacora(usuarioId, ordId, desc, sistema);
            await BitacoraIServ.AddBitacora(bita);
        }

        [Inject]
        public I110UsuariosServ UsuariosIServ { get; set; }
        [Parameter]
        public Z110_Usuarios ElUsuario { get; set; } = new();
        
        
    }
}

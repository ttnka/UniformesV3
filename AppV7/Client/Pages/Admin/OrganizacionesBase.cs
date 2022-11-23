using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen.Blazor;
using System.Diagnostics;

namespace AppV7.Client.Pages.Admin
{
    public class OrganizacionesBase : ComponentBase 
    {
        [Inject]
        public I100OrgServ OrgIServ { get; set; } = default!;
        public IEnumerable<Z100_Org> LasOrgs { get; set; } = 
            Enumerable.Empty<Z100_Org>();
        

        [Parameter]
        public Dictionary<string, string> OrgDic { get; set; } = new(); 
        public bool isLoading { get; set; } = false;
        public bool Editando { get; set; } = false;
        public bool HayWebMaster { get; set; } = false;
        [Inject]
        public NavigationManager NM { get; set; } = default!;
        public RadzenDataGrid<Z100_Org>? OrgGrid { get; set; } = default!;
        protected async override Task OnInitializedAsync()
        {
            await LeerDatos();
            await Escribir(ElUsuario.UsuariosId, ElUsuario.OrgId,
                            "Consulta de las Organizaciones desde sistema", false);
        }
        
        protected async Task LeerDatos()
        {
            isLoading = true;
            LasOrgs = await OrgIServ.Buscar("Allo");
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

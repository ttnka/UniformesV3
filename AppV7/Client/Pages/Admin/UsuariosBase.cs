using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace AppV7.Client.Pages.Admin
{
    public class UsuariosBase : ComponentBase 
    {
        [Inject]
        public I100OrgServ OrgIServ { get; set; }
        [Inject]
        public I110UsuariosServ UserIServ { get; set; }
        
        [Parameter]
        public Z110_Usuarios ElUsuario { get; set; }
        public IEnumerable<Z110_Usuarios> LosUsers { get; set; } = 
            new List<Z110_Usuarios>();
        public IEnumerable<Z100_Org> LasOrgs { get; set; } =
                Enumerable.Empty<Z100_Org>();
        public List<KeyValuePair<int, string>> LosNiveles { get; set; } =
            new List<KeyValuePair<int, string>>();
        [Parameter]
        public Dictionary<string, string> DatosDic { get; set; } = 
            new Dictionary<string, string>();   
        public RadzenDataGrid<Z110_Usuarios> UsersGrid { get; set; } =
            new RadzenDataGrid<Z110_Usuarios>();
        public bool Editando { get; set; } = false;

        protected async override Task OnInitializedAsync()
        {
            await LeerUsers();
            await LeerOrgs();
            
        }

        public async Task LeerUsers()
        {
            LosUsers = await UserIServ.Buscar("All", "Vacio");
        }

        public async Task LeerOrgs()
        {
            LasOrgs = await OrgIServ.Buscar("All");
            if (LasOrgs != null)
            {
                foreach (var orgt in LasOrgs)
                {
                    if (!DatosDic.ContainsKey($"Org_{orgt.OrgId}"))
                        DatosDic.Add($"Org_{orgt.OrgId}", orgt.Comercial);
                }
            }
            if (!DatosDic.ContainsKey("UsersNivel"))
            {
                string[] titulos = UserNivel.Titulos.Split(",");
                for (int i = 0; i < titulos.Length; i++)
                {
                    if (ElUsuario.Nivel > i +1 ) 
                        LosNiveles.Add(new KeyValuePair<int, string>(i+1, titulos[i]));
                    DatosDic.Add($"Nivel_{i + 1}", titulos[i]);
                }
                DatosDic.Add("UsersNivel", "Ok");
            }

        }

        [Inject]
        NavigationManager NM { get; set; }
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
    }
}

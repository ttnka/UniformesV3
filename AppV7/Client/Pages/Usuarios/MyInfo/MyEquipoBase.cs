using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace AppV7.Client.Pages.Usuarios.MyInfo
{
    public class MyEquipoBase : ComponentBase 
    {
        public bool Permiso { get; set; } = true;
        public bool Editando { get; set; } = false;
        [Parameter]
        public Z110_Usuarios ElUsuario { get; set; } = new();
        [Inject]
        public I110UsuariosServ UserIServ { get; set; } = default!;
        public IEnumerable<Z110_Usuarios> ElTeam { get; set; } = new List<Z110_Usuarios>();
        [Inject]
        public I100OrgServ OrgIServ { get; set; } = default!;
        [Parameter]
        public Dictionary<string, string> DataDic { get; set; } = 
            new Dictionary<string, string>();
        public IEnumerable<Z100_Org> Orgs { get; set; } = new List<Z100_Org>();
        public List<KeyValuePair<int, string>> LosNiveles { get; set; } =
            new List<KeyValuePair<int, string>>();

        public RadzenDataGrid<Z110_Usuarios>? UsersGrid { get; set; } = default!;
           //new RadzenDataGrid<Z110_Usuarios>();

        protected async override Task OnInitializedAsync()
        {
            await LeerDatos();
            await LeerTeam();
            LeerNiveles();
        }

        public async Task LeerDatos()
        {
            string filtro = ElUsuario.Nivel > 5 ? "Activos" : $"Uno_-_OrgId_-_{ElUsuario.OrgId}";
            Orgs = await OrgIServ.Buscar(filtro);
            foreach (var OrgT in Orgs)
            {
                if (!DataDic.ContainsKey($"Org_{OrgT.OrgId}"))
                { 
                    //DataDic.Add($"Org2_{OrgT.OrgId}", $"{OrgT.Comercial} {OrgT.Rfc}");
                    DataDic.Add($"Org_{OrgT.OrgId}", $"{OrgT.Comercial}");
                }
            }
        }
        public async Task LeerTeam()
        {
            ElTeam = await UserIServ.Buscar("Organizacion", ElUsuario.OrgId);
        }

        public void LeerNiveles() {
            string[] NomNiveles = UserNivel.Titulos.Split(",");
            for (var i = 0; i < NomNiveles.Length; i++) 
            {
                if (ElUsuario.Nivel > i+1)
                {
                    LosNiveles.Add(new KeyValuePair<int, string>(i+1, NomNiveles[i]));
                }
                if (!DataDic.ContainsKey($"Nivel_{i + 1}"))
                    DataDic.Add($"Nivel_{i + 1}", NomNiveles[i]);
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

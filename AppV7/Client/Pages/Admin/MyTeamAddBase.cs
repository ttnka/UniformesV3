using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace AppV7.Client.Pages.Admin
{
    public class MyTeamAddBase : ComponentBase 
    {
        [Inject]
        public I100OrgServ OrgIServ { get; set; }
        [Inject]
        public IAddUsuarioServ AddUserIServ { get; set; }
        public EAddUsuario NewAddUsuario { get; set; } = new();
        public List<KeyValuePair<int, string>> LosNiveles { get; set; } =
            new List<KeyValuePair<int, string>>();
        public RadzenTemplateForm<EAddUsuario> AddUsuarioForm { get; set; } = new();
        public IEnumerable<Z100_Org> LasOrgs { get; set; } =
                Enumerable.Empty<Z100_Org>();
        [Parameter]
        public Z110_Usuarios ElUsuario { get; set; } = new();
        [Parameter]
        public Dictionary<string, string> DataDic { get; set; } = 
            new Dictionary<string, string>();
        protected override async Task OnInitializedAsync()
        {
            NewAddUsuario.OrgId = ElUsuario.OrgId;
            LeerNiveles();
            await LeerOrganizaciones();

            //await Escribir(ElUsuario.UsuariosId, ElUsuario.OrgId, 
            //    "Consulto Crear nuevo usuario", false);
        }

        protected void LeerNiveles()
        {
            string[] NomNiveles = UserNivel.Titulos.Split(",");
            for (var i = 0; i < NomNiveles.Length; i++)
            {
                if (ElUsuario.Nivel > i + 1)
                {
                    LosNiveles.Add(new KeyValuePair<int, string>(i + 1, NomNiveles[i]));
                }
                if (!DataDic.ContainsKey($"Nivel_{i + 1}"))
                    DataDic.Add($"Nivel_{i + 1}", NomNiveles[i]);
            }
        }
        protected async Task LeerOrganizaciones()
        {
            string filtro = ElUsuario.Nivel > 5 ? "Activos" : $"Uno_-_OrgId_-_{ElUsuario.OrgId}";
            LasOrgs = await OrgIServ.Buscar(filtro);
            foreach (var OrgT in LasOrgs)
            {
                if (!DataDic.ContainsKey($"Org_{OrgT.OrgId}"))
                    DataDic.Add($"Org_{OrgT.OrgId}", $"{OrgT.Comercial}");
            }
        }
        public async Task SaveNewUsuario()
        {
            await AddUserIServ.AddUsuario(NewAddUsuario);
            string texto = $"Se creo un nuevo usuario {NewAddUsuario.Mail} ";
            texto += $"del equipo {DataDic[$"Org_{ElUsuario.OrgId}"]}";
                await Escribir(ElUsuario.UsuariosId, ElUsuario.OrgId, texto, false);
            NewAddUsuario = new();
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

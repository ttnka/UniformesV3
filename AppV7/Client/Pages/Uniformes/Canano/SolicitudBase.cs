using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared.Libreria;
using AppV7.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen.Blazor;

namespace AppV7.Client.Pages.Uniformes.Canano
{
    public class SolicitudBase : ComponentBase 
    {
        [Inject]
        public I210AlmacenServ AlmIServ { get; set; } = default!;
        [Inject]
        public I110UsuariosServ UserIServ { get; set; } = default!;
        [Inject]
        public I230SolicitudServ SolIServ { get; set; } = default!;
        [Inject]
        public I232DetSolServ DetIServ { get; set; } = default!;
        public IEnumerable<Z210_Almacen> LosAlmacenes { get; set; } =
            Enumerable.Empty<Z210_Almacen>();
        public IEnumerable<Z230_Solicitud> LasSolicitudes { get; set; } =
            Enumerable.Empty<Z230_Solicitud>();
        public List<KeyValuePair<string, string>> LosUsers { get; set; } =
            new List<KeyValuePair<string, string>>();
        public List<string> LosTipos { get; set; } = new();
        public List<KeyValuePair<int, string>> LosEdos { get; set; } =
            new List<KeyValuePair<int, string>>();
        public bool Editando { get; set; } = false;
        public Dictionary<string, string> DataDic { get; set; } = new();
        public NavigationManager NM { get; set; } = default!;
        public RadzenDataGrid<Z230_Solicitud>? SolGrid { get; set; } = default!;
        protected async override Task OnInitializedAsync()
        {
            var autState = await AuthStateTask;
            var user = autState.User;
            if (!user.Identity.IsAuthenticated) NM.NavigateTo("/firma?laurl=/inicio");
            UserIdLogAll = user.FindFirst(c => c.Type == "sub")?.Value!;
            var UserList = await UserIServ.Buscar($"UserId_-_UserId_-_{UserIdLogAll}", "vacio");
            ElUsuario = UserList.FirstOrDefault()!;

            LeerTipos();
            await LeerDatos();
            await LeerAlmacenes();
            await LeerDetalle();
            if (ElUsuario.Nivel > 2) await LeerUsers();
            await Escribir(ElUsuario.UsuariosId, ElUsuario.OrgId,
                            "Consulta del listado de solicitudes", false);
        }
        public async Task LeerDatos()
        {
            string buscar = ElUsuario.Nivel > 2 ? "Alla" :
                            $"UserSol_-_UserSol_-_{ElUsuario.UsuariosId}";
            LasSolicitudes = await SolIServ.Buscar(buscar);
            if (LasSolicitudes.Any())
            {
                foreach(var sol in LasSolicitudes)
                {
                    if (!DataDic.ContainsKey($"Sol_{sol.Folio}"))
                        DataDic.Add($"Sol_{sol.Folio}", sol.Folio);
                }
            }
        }
        public async Task LeerDetalle()
        {
            var DetTemp = await DetIServ.Buscar("Alla");
            foreach(var sol in LasSolicitudes)
            {
                if(!DataDic.ContainsKey($"Fol_Det_{sol.Folio}")) 
                    DataDic.Add($"Fol_Det_{sol.Folio}", "0");
                int reg = DetTemp.Count(x => x.Folio.Equals(sol.Folio) &&  x.Status);
                if (reg > 0) DataDic[$"Fol_Det_{sol.Folio}"] = reg.ToString();
            }
        }
        public async Task LeerUsers()
        {
            var UsersTemp = await UserIServ.Buscar("Alla", "Vacio");
            foreach(var usr in UsersTemp)
            {
                if (!DataDic.ContainsKey($"User_{usr.UsuariosId}"))
                {
                    string nombre = usr.Nombre == usr.OldEmail ? usr.OldEmail :
                        $"{usr.Nombre} {usr.Paterno} {usr.OldEmail}";

                    DataDic.Add($"User_{usr.UsuariosId}", nombre);
                    LosUsers.Add(new KeyValuePair<string, string>(usr.UsuariosId, nombre));
                }
            }
        }
        public async Task LeerAlmacenes()
        {
            LosAlmacenes = await AlmIServ.Buscar("Alla");
        }
        public void LeerTipos()
        {
            string[] tiposTemp = Constantes.Tipos.Split(",");
            foreach (var tipo in tiposTemp)
            {
                LosTipos.Add(tipo);
            }
            string[] eti = { "success", "info", "warning" };
            var SolEdo = Constantes.SolEdos.Split(",");
            if (!DataDic.ContainsKey("Sol_Edo"))
            {
                DataDic.Add("Sol_Edo", "Ok");
                for (var i=0; i < SolEdo.Length; i++ )
                {
                    DataDic.Add($"Sol_Edo_{i+1}", SolEdo[i]);
                    DataDic.Add($"Etiqueta_{i+1}", $"etiqueta {eti[i]}");
                }
                
                if (ElUsuario.Nivel > 2)
                    LosEdos.Add(new KeyValuePair<int, string>(1, "Entregado"));
                LosEdos.Add(new KeyValuePair<int, string>(2, "Proceso"));
                LosEdos.Add(new KeyValuePair<int, string>(3, "Cancelada"));
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
        public Z110_Usuarios ElUsuario { get; set; } = new();

        public async Task<string> ElFolio()
        {
            int seg = DateTime.Now.Second;
            seg = seg > 24 ? seg - 24 : seg;
            seg = seg > 24 ? seg - 24 : seg;
            string MinArray = "abcdefghijklmnopqrstuvwxyz";
            string resultado = MinArray.Substring(seg, 1);
            var rest = await SolIServ.Buscar("Alla");
            if (rest.Count() < 10) resultado += "0"; 
            resultado += (rest.Count()+1).ToString();
            return resultado;
        }
    }
}

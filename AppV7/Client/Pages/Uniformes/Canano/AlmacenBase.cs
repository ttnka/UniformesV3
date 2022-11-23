using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen.Blazor;

namespace AppV7.Client.Pages.Uniformes.Canano
{
    public class AlmacenBase : ComponentBase
    {
        [Inject]
        public I210AlmacenServ AlmIServ { get; set; } = default!;
        [Inject]
        public I110UsuariosServ UserIServ { get; set; } = default!;
        public IEnumerable<Z210_Almacen> LosAlmacenes { get; set; } = 
            Enumerable.Empty<Z210_Almacen>();
        public List<string> LosMpios { get; set; } = new List<string>();
        public bool Editando { get; set; } = false;
        public Dictionary<string, Z210_Almacen> AlmDic { get; set; } = new();
        public NavigationManager NM { get; set; } = default!;
        public RadzenDataGrid<Z210_Almacen>? AlmGrid { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            var autState = await AuthStateTask;
            var user = autState.User;
            if (!user.Identity.IsAuthenticated) NM.NavigateTo("/firma?laurl=/inicio");
            UserIdLogAll = user.FindFirst(c => c.Type == "sub")?.Value!;
            var UserList = await UserIServ.Buscar($"UserId_-_UserId_-_{ UserIdLogAll}", "vacio");
            ElUsuario = UserList.FirstOrDefault()!;
            LeerMunicipios();
            await LeerDatos();
            await Escribir(ElUsuario.UsuariosId, ElUsuario.OrgId,
                            "Consulta del listado de almacenes", false);
        }

        protected void LeerMunicipios()
        {
            string[] NomMpios = Constantes.MpiosTodos.Split(",");
            for (int i = 0; i < NomMpios.Length; i++)
            {
                LosMpios.Add(NomMpios[i]);
            }
        }
        public async Task LeerDatos()
        {
            LosAlmacenes = await AlmIServ.Buscar("Alla");
            if (LosAlmacenes.Any())
            {
                foreach(var Alm in LosAlmacenes)
                {
                    if (!AlmDic.ContainsKey(Alm.Corto)) AlmDic.Add(Alm.Corto, Alm);    
                }
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

    }
}

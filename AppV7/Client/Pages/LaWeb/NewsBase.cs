using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen.Blazor;

namespace AppV7.Client.Pages.LaWeb
{
    public class NewsBase : ComponentBase
    {
        [Inject]
        public I810GeneralServ GeneralIServ { get; set; } = default!;
        [Inject] I812FilesServ FilesIServ { get; set; } = default!;
        public IEnumerable<Z812_Files> LosArchivos { get; set; } = null!;

        public IEnumerable<Z810_General> LasNews { get; set; } = null!;
        //Enumerable.Empty<Z800_WebSite>();
        public string ElPath { get; set; } = @"Imagenes\News\";
        [Inject]
        public NavigationManager NM { get; set; } = default!;
        public RadzenDataGrid<Z810_General>? NewsGrid { get; set; } = default!;
        public bool Editando = false;
        public bool Mostrar { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            await Autentico();
            await LeerDatos();
            await LeerArchivos();
        }
        public async Task LeerDatos()
        {
            LasNews = await GeneralIServ.Buscar("Alla");
            Mostrar = true;
        }

        public async Task LeerArchivos()
        {
            LosArchivos = await FilesIServ.Buscar("Alla");
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
        public async Task<Z110_Usuarios> Autentico() 
        {
            if (ElUsuario == null)
            {
                var autState = await AuthStateTask;
                var user = autState.User;

                if (!user.Identity.IsAuthenticated)
                {
                }
                UserIdLogAll = user.FindFirst(c => c.Type == "sub")?.Value!;
            }

            return ElUsuario;
        }
    }
}

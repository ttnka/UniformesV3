using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared.Libreria;
using AppV7.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace AppV7.Client.Pages.Uniformes
{
    public class InicioUniformesBase : ComponentBase 
    {
        public int SelectedIndex { get; set; } = 0;
        public int showRenglon { get; set; } = 1;
        public double showavance { get; set; } = 2;
        
        public Dictionary<string, string> Graficas { get; set; } = 
            new Dictionary<string, string>();

        protected override async Task OnInitializedAsync()
        {
            await LeerGraficos();
            showRenglon = MyFunc.DameRandom(1, 100);
            showavance = MyFunc.DameRandom(1, 100);
        }

        protected async Task LeerGraficos()
        {
            // General  Folios Entregados /  Meta GENERAL 

            // Almacenes y Comercioss sumatoria de los 2

            // Meta vs Almancen vs Folios


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
        public Z110_Usuarios ElUsuarioAll { get; set; } = new();

    }
}

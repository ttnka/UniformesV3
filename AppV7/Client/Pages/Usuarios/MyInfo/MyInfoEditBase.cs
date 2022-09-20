using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace AppV7.Client.Pages.Usuarios.MyInfo
{
    public class MyInfoEditBase : ComponentBase 
    {
        [Inject]
        public I110UsuariosServ UserIServ { get; set; }
        [Parameter]
        public Z110_Usuarios ElUsuarioPara { get; set; }
        
        public Z110_Usuarios ElUsuario { get; set; } = new();
        public string LaOrg { get; set; } = "Vacia";
        public string ElNiv { get; set; } = "Vacio";

        [Parameter]
        public Dictionary<string, string> DatosDic { get; set; } =
            new Dictionary<string, string>();

        public bool Habilitar = false;

        [Inject]
        NavigationManager NM { get; set; } 
        protected override async Task OnInitializedAsync()
        {
            LeerUsuario(); 
            LeerInfo();
            await Escribir(ElUsuario.UsuariosId, ElUsuario.OrgId,
                "Consulto sus datos", false);
        }
        
        public void LeerUsuario()
        {
            ElUsuario = ElUsuarioPara;
        }
        public void LeerInfo()
        {
            if (DatosDic.ContainsKey($"Org_{ElUsuario.OrgId}"))
                LaOrg = DatosDic[$"Org_{ElUsuario.OrgId}"];

            if (DatosDic.ContainsKey($"Nivel_{ElUsuario.Nivel}"))
                ElNiv = DatosDic[$"Nivel_{ElUsuario.Nivel}"];
        }
        public async Task ActualizarMisDatos()
        {
            await UserIServ.UpDateUsuario(ElUsuario);
            Habilitar = false;
            /*
            string modif = $"Modifico sus datos {ElUsuario.Nombre} {ElUsuario.Paterno} ";
            modif += $"{ElUsuario.Materno}";
            await Escribir(ElUsuario.UsuariosId, ElUsuario.OrgId,
                modif , false);
            */
        }

        //[CascadingParameter]
        //public Task<AuthenticationState> AuthStateTask { get; set; }
        //[Parameter]
        //public string UserIdLog { get; set; }
        
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

using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using AppV7.Shared;
using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;

namespace AppV7.Client.Pages.Sistema
{
    public class AddUsuarioBase : ComponentBase
    {
        [Inject]
        public I100OrgServ OrgIServ { get; set; } = default!;
        [Inject]
        public IAddUsuarioServ AddUserIServ { get; set; } = default!;
        public EAddUsuario NewAddUsuario { get; set; } = new();
        public List<KeyValuePair<int, string>> LosNiveles { get; set; } =
            new List<KeyValuePair<int, string>>();
        public RadzenTemplateForm<EAddUsuario>? AddUsuarioForm { get; set; } = new();
        public IEnumerable<Z100_Org> LasOrgs { get; set; } =
                Enumerable.Empty<Z100_Org>();

        protected override async Task OnInitializedAsync()
        {
            await LeerUser();
            LeerNiveles();
            await LeerOrganizaciones();
            
            await Escribir(ElUsuario.UsuariosId, ElUsuario.OrgId, 
                "Consulto Crear nuevo usuario", false);
        }
        protected void LeerNiveles() 
        { 
            string[] NomNiveles =  UserNivel.Titulos.Split(",");
            
            for (int i = 0; i < NomNiveles.Length ; i++)
            {   
                if (i == ElUsuario.Nivel) break;
                LosNiveles.Add(new KeyValuePair<int, string>(i+1, NomNiveles[i]));
            }
        }
        protected async Task LeerOrganizaciones()
        {
             LasOrgs = await OrgIServ.Buscar("Allo");
        }
        public async Task SaveNewUsuario() 
        {
            IsRegistro = false;
            await AddUserIServ.AddUsuario(NewAddUsuario);
            NM.NavigateTo("/usuarios");    
        }
        
        [Inject]
        public I190BitacoraServ BitacoraIServ { get; set; } = default!;
        public MyFunc MyFunc { get; set; } = new MyFunc();
        public async Task Escribir(string usuarioId, string ordId,
            string desc, bool sistema)
        {
            Z190_Bitacora bita = new ();
            bita = MyFunc.WriteBitacora(usuarioId, ordId, desc, sistema);
            await BitacoraIServ.AddBitacora(bita);
        }

        public NotificationMessage ElMsn(string tipo, string titulo, string mensaje, int duracion)
        {
            NotificationMessage respuesta = new();
            switch (tipo.ToLower())
            {
                case "info":
                    respuesta.Severity = NotificationSeverity.Info;
                    break;
                case "error":
                    respuesta.Severity = NotificationSeverity.Error;
                    break;
                case "warning":
                    respuesta.Severity = NotificationSeverity.Warning;
                    break;
                default:
                    respuesta.Severity = NotificationSeverity.Success;
                    break;
            }
            respuesta.Summary = titulo;
            respuesta.Detail = mensaje;
            respuesta.Duration = 4000 + duracion;
            return respuesta;
        }

        [CascadingParameter]
        public Task<AuthenticationState> AuthStateTask { get; set; } = default!;
        [Inject]
        public I110UsuariosServ UsersIServ { get; set; } = default!;
        public Z110_Usuarios ElUsuario { get; set; } = new();
                public bool Mayuscula { get; set; } = false;
        protected string MayArray = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public bool Minuscula { get; set; } = false;
        protected string MinArray = "abcdefghijklmnopqrstuvwxyz";
        public bool Numero { get; set; } = false;
        protected string NumArray = "0123456789";
        public bool Punto { get; set; } = false;
        protected string PuntoArray = "-._@+";
        protected string Todos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        public bool IsValido = false;
        public bool IsLargo = false;
        public bool IsRegistro = false;
        public string borrar = string.Empty;
        public void Valido ()
        {
            int sigL = 0;        
            foreach (var s in NewAddUsuario.Pass )
            {
                sigL = 0;
                if (IsValido = Todos.Contains(s))
                {
                    if(!Mayuscula && sigL == 0)
                    {
                        if (Mayuscula = MayArray.Any(a => a == s)) sigL = 1;
                    }
                    if(!Minuscula && sigL == 0)
                    {
                        if (Minuscula = MinArray.Any(i => i == s)) sigL = 1;
                    }
                    if(!Numero && sigL == 0 )
                    {
                        if(Numero = NumArray.Any(n => n == s)) sigL = 1;
                    }
                    /*
                    if(!Punto)
                    {
                        if(Punto = PuntoArray.Any(p => p==s)) sigL = 1;
                    }
                    */
                }
            }
            IsLargo = NewAddUsuario.Pass.Length > 5;
            IsRegistro = IsValido && IsLargo && Mayuscula && Minuscula && Numero;
        }
        [Inject]
        NavigationManager NM { get; set; } = default!;
        public async Task LeerUser()
        {
            var autState = await AuthStateTask;
            var user = autState.User;
            if (!user.Identity!.IsAuthenticated) NM.NavigateTo("/firma?laurl=/inicio");
            var UserIdLogAll = user.FindFirst(c => c.Type == "sub")?.Value!;
            /*
            LosUsers = await UserIServ.Buscar("Allo", "Vacio");
            ElUsuario = LosUsers.FirstOrDefault(x => x.UsuariosId == UserIdLogAll)!;
            */
            var UserList = await UsersIServ.Buscar($"UserId_-_UserId_-_{UserIdLogAll}", "vacio");
            ElUsuario = UserList.FirstOrDefault()!;
            
        }
    }
}


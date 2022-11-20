using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using AppV7.Shared;
using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared.Libreria;

namespace AppV7.Client.Pages.Sistema
{
    public class AddUsuarioBase : ComponentBase
    {
        [Inject]
        public I100OrgServ OrgIServ { get; set; } = default!;
        [Inject]
        public IAddUsuarioServ AddUserIServ { get; set; } = default!;

        public EAddUsuario NewAddUsuario { get; set; } = new()
        {
            Mail = "",
            Pass = "",
            Nivel = 1
        };
        public List<KeyValuePair<int, string>> LosNiveles { get; set; } =
            new List<KeyValuePair<int, string>>();
        public RadzenTemplateForm<EAddUsuario>? AddUsuarioForm { get; set; } = new();
        public IEnumerable<Z100_Org> LasOrgs { get; set; } =
                Enumerable.Empty<Z100_Org>();
        
        protected override async Task OnInitializedAsync()
        {
            //NewAddUsuario.OrgId = Constantes.PgRfc; // ERRO orgId no es RFC
            await PoblarUsuario($"{Constantes.SyMail}");
            
            NewAddUsuario.OrgId = await AsignaOrgId(Constantes.PgRfc);
            
            LeerNiveles();
            await LeerOrganizaciones();
            
            
            //string texto = $"Se actualizo un registro {org.Rfc}";
            //texto += $"{org.Comercial} {org.RazonSocial}";
            await Escribir(ElUsuario.UsuariosId, ElUsuario.OrgId, "Consulto Crear nuevo usuario", false);
            Console.WriteLine($"mail {NewAddUsuario.Mail} pass {NewAddUsuario.Pass}");
        }
        protected async Task<string> AsignaOrgId(string rfc)
        {
            var resultado = await OrgIServ.Buscar($"Rfc_-_Rfc_-_{rfc}");

            return resultado.FirstOrDefault().OrgId;
        }

        protected void LeerNiveles() 
        { 
            string[] NomNiveles =  UserNivel.Titulos.Split(",");
            
            switch (NewAddUsuario.Nivel)
            {
                case 1:
                    LosNiveles.Add(new KeyValuePair<int, string>(1, NomNiveles[0].ToString()));
                    break;
                default:
                        for (int i = 1; i < NomNiveles.Length-1; i++)
                        { 
                            LosNiveles.Add(new KeyValuePair<int, string>(i, NomNiveles[i]));
                        }
                    break;
            }
        }
        protected async Task LeerOrganizaciones()
        {
            string LaClave = $"Rfc_-_Rfc_-_{Constantes.PgRfc}";
            switch (NewAddUsuario.Nivel)
            {
                case 5:
                    LaClave = "Activos";  
                    break;
                case int n when(n >= 2):
                    LaClave = $"Rfc_-_Rfc_-_{Constantes.PgRfc}"; // aqui va la empresa del usuario
                    break;    
            }
            LasOrgs = await OrgIServ.Buscar(LaClave);
            
        }
        public async Task SaveNewUsuario() 
        {
            await AddUserIServ.AddUsuario(NewAddUsuario);
            NM.NavigateTo("/");    
        }
        [Inject]
        NavigationManager NM { get; set; } = default!;
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
        [Inject]
        public I110UsuariosServ UsuariosIServ { get; set; } = default!;
        public Z110_Usuarios ElUsuario { get; set; } = new();
        protected async Task PoblarUsuario(string mail)
        {
            var resultado = await UsuariosIServ.Buscar($"OldEmail_-_OldEmail_-_{mail}",
                "vacio");
            ElUsuario = resultado.FirstOrDefault() ?? new();
        }

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
    }
}

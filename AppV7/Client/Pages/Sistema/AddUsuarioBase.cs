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
        public I100OrgServ OrgIServ { get; set; }
        [Inject]
        public IAddUsuarioServ AddUserIServ { get; set; }
        
        public EAddUsuario NewAddUsuario { get; set; } = new();
        public List<KeyValuePair<int, string>> LosNiveles { get; set; } = 
            new List<KeyValuePair<int, string>>();
        public RadzenTemplateForm<EAddUsuario> AddUsuarioForm { get; set; } = new();
        public IEnumerable<Z100_Org> LasOrgs { get; set; } = 
                Enumerable.Empty<Z100_Org>();
        
        protected override async Task OnInitializedAsync()
        {
            //NewAddUsuario.OrgId = Constantes.PgRfc; // ERRO orgId no es RFC
            await PoblarUsuario($"{Constantes.SyMail}");
            NewAddUsuario.OrgId = await AsignaOrgId(Constantes.PgRfc);
            NewAddUsuario.Nivel = 1;
            LeerNiveles();
            await LeerOrganizaciones();

            //string texto = $"Se actualizo un registro {org.Rfc}";
            //texto += $"{org.Comercial} {org.RazonSocial}";
            await Escribir(ElUsuario.UsuariosId, ElUsuario.OrgId, "Consulto Crear nuevo usuario", false);
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
        [Inject]
        public I110UsuariosServ UsuariosIServ { get; set; }
        public Z110_Usuarios ElUsuario { get; set; } = new();
        protected async Task PoblarUsuario(string mail)
        {
            var resultado = await UsuariosIServ.Buscar($"OldEmail_-_OldEmail_-_{mail}",
                "vacio");
            ElUsuario = resultado.FirstOrDefault() ?? new();
        }
    }
}

using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace AppV7.Client.Pages.Sistema
{
    public class BitacoraBase : ComponentBase 
    {
        [Inject]
        public I190BitacoraServ BitacoraIServ { get; set; }
        public IEnumerable<Z190_Bitacora> LasBitacoras { get; set; } =
                    Enumerable.Empty<Z190_Bitacora>();
        public RadzenDataGrid<Z190_Bitacora>? BitacoraGrid { get; set; } = default!;
            //new RadzenDataGrid<Z190_Bitacora>();
        [Parameter]
        public Dictionary<string, string> NombresDic { get; set; } = new();

        [Inject]
        public I100OrgServ OrgIServ { get; set; }
        public IEnumerable<Z100_Org> LasOrgs { get; set; } = 
            Enumerable.Empty<Z100_Org>();
        public Z100_Org LasOrgX { get; set; } = new();
        public string OrgX { get; set; } = string.Empty;
        public bool ElSistema { get; set; } = false;
        public string ElUser { get; set; } = string.Empty;
        public MyFunc MyFunc { get; set; } = new(); 
        protected override async Task OnInitializedAsync()
        {
            await LeerDatos("All","",false);   // Lee los datos de bitacora
            await LeerNombres(LasBitacoras);   // Lee los nombres de creadores bitacoras
            
            // Lee los datos del UserLogiado esto va a cambiar
            await PoblarUsuario($"{Constantes.SyMail}");
            await LeerOrganizaciones(2);
            
            await Escribir(ElUsuario.UsuariosId, ElUsuario.OrgId,
                            "Consulta de las Organizaciones", false);
            
            
        }
        
        public async Task LeerOrganizaciones(int nivel)
        {
            string LaClave = $"Rfc_-_Rfc_-_{Constantes.PgRfc}";
            switch (nivel)
            {
                case 2:
                    LaClave = "Activos";
                    break;
                case 1:
                    LaClave = $"Rfc_-_Rfc_-_{Constantes.PgRfc}"; // aqui va la empresa del usuario
                    break;
            }
            LasOrgs = await OrgIServ.Buscar(LaClave);

        }
        public async Task LeerDatos(string? orgX, string? elUsuario, bool elsistema )
        { // lee los registros de bitacora que le pedimos
            if (elsistema)
            {
                LasBitacoras = await BitacoraIServ.Buscar("Sistema", "Vacio");
            }
            else
            {
                LasBitacoras = await BitacoraIServ.Buscar("All", "Vacio");
            }
        }

        public async Task LeerNombres(IEnumerable<Z190_Bitacora> lBitacoras)
        { // lee
            if (lBitacoras == null) return;
                foreach (var lb in lBitacoras)
            {
                await GetNombres(lb.UsuariosId) ;
                //Console.WriteLine(lb.UsuariosId);
                
            }
        }

        public async Task GetNombres(string userId)
        {   // pone los nombres de los usuarios en un diccionario 
            if (!NombresDic.ContainsKey(userId))
            {
                IEnumerable<Z110_Usuarios> ListUsuarios = new List<Z110_Usuarios>();
                ListUsuarios = await UsuariosIServ.Buscar($"UsuariosId_-_UsuariosId_-_{userId}", "Vacio");
                if (ListUsuarios.Any())
                { 
                    //NombresDic.Add(userId, ListUsuarios.FirstOrDefault()!);
                    string nombre = $"{ListUsuarios.FirstOrDefault().Nombre} ";
                    nombre += $"{ListUsuarios.FirstOrDefault().Paterno} ";
                    nombre += $"{ListUsuarios.FirstOrDefault().Materno}";
                    NombresDic.Add(userId, nombre);
                }
                else
                {
                    NombresDic.Add(userId, "Vacio");
                }
            }
        }
        public async Task Escribir(string usuarioId, string ordId,
            string desc, bool sistema)
        { // escribe en la bitacora 
            Z190_Bitacora bita = new Z190_Bitacora();
            bita = MyFunc.WriteBitacora(usuarioId, ordId, desc, sistema);
            await BitacoraIServ.AddBitacora(bita);
        }
        [Inject]
        public I110UsuariosServ UsuariosIServ { get; set; }
        public Z110_Usuarios ElUsuario { get; set; } = new();
        protected async Task PoblarUsuario(string mail)
        {  // da un userLogiado va a cambiar esto
            var resultado = await UsuariosIServ.Buscar($"OldEmail_-_OldEmail_-_{mail}",
                "vacio");
            ElUsuario = resultado.FirstOrDefault() ?? new();
        }
    }
}

using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared.Libreria;
using AppV7.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using Radzen.Blazor;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace AppV7.Client.Pages.Uniformes.Folios
{
    public class InventarioBase : ComponentBase
    {
        [Inject]
        public I230SolicitudServ SolIServ { get; set; } = default!;
        [Inject]
        public I232DetSolServ DetIServ { get; set; } = default!;
        [Inject]
        public I210AlmacenServ AlmIServ { get; set; } = default!;
        [Inject]
        public I220ProductoServ ProdIServ { get; set; } = default!;
        public IEnumerable<Z210_Almacen> LosAlmacenes { get; set; } =
            Enumerable.Empty<Z210_Almacen>();
        public List<string> LosMpios { get; set; } = new();
        public List<string> LosTipos { get; set; } = new();
        public IEnumerable<Z230_Solicitud> LasSolicitudes { get; set; } =
            new List<Z230_Solicitud>();
        public IEnumerable<Z220_Producto> LosProductos { get; set; } =
            new List<Z220_Producto>();
        public List<Z232_DetSol> LosDet { get; set; } =
            new List<Z232_DetSol>();
        public List<KeyValuePair<int, string>> LosEdos { get; set; } =
            new List<KeyValuePair<int, string>>();
        public List<ReporteDet> ElRepDet { get; set; } = new List<ReporteDet>();
        public Filtro ElFiltro { get; set; } = new();
        public bool Datos { get; set; } = true;
        public bool Exportar { get; set; } = false;
        public RadzenTemplateForm<Filtro>? FiltroTemp { get; set; } = default!;
        public RadzenDataGrid<ReporteDet>? RepDetGrid { get; set; } = default!;
        protected async override Task OnInitializedAsync()
        {
            await LeerUser();
            await LeerLaData();
            await Escribir(ElUsuario.UsuariosId, ElUsuario.OrgId,
                            "Consulta del listado Inventarios", false);
            LeerConstantes();
        }
        public async Task LeerLaData()
        {
            LasSolicitudes = await SolIServ.Buscar("Alla");
            LosAlmacenes = await AlmIServ.Buscar("Alla");
            LosProductos = await ProdIServ.Buscar("Alla");
        }
        
        public async Task LeerRegistros()
        {
            ElFiltro.Filtrado = false;
            if (string.IsNullOrEmpty(ElFiltro.FolioF)) 
                ElFiltro.FolioF = "Alla";
            if (string.IsNullOrEmpty(ElFiltro.AlmacenF))
                ElFiltro.AlmacenF = "Alla";
            if (string.IsNullOrEmpty(ElFiltro.TipoEntradaF))
                ElFiltro.TipoEntradaF = "Alla";
            if (ElUsuario.Nivel == 2)
            {
                ElFiltro.ComercioF = ElUsuario.UsuariosId;
            }
            else
            {
                if (string.IsNullOrEmpty(ElFiltro.ComercioF))
                    ElFiltro.ComercioF = "Alla";
            }
            
            if (string.IsNullOrEmpty(ElFiltro.ProductoF))
                ElFiltro.ProductoF = "Alla";
            if (string.IsNullOrEmpty(ElFiltro.CiudadF))
                ElFiltro.CiudadF = "Alla";

            string buscar = $"Inventarios_-_FoliosF_-_{ElFiltro.FolioF}_-_";
            buscar += $"EstadoF_-_{ElFiltro.EstadoF}_-_AlmacenF_-_{ElFiltro.AlmacenF}_-_";
            buscar += $"TipoEntradaF_-_{ElFiltro.TipoEntradaF}_-_ComercioF_-_{ElFiltro.ComercioF}_-_";
            buscar += $"ProductoF_-_{ElFiltro.ProductoF}_-_CiudadF_-_{ElFiltro.CiudadF}_-_";
            buscar += $"CantidadF_-_{ElFiltro. CantidadF}";

            var resultado = await DetIServ.Buscar(buscar);
            if (resultado.Any())
            { 
                ElRepDet = LlenadoReporte(resultado); 
            }
            else { ElRepDet = null; }

            await Escribir(ElUsuario.UsuariosId, ElUsuario.OrgId,
                            $"Solicito un reporte de inventario de {buscar}", false);
            Datos = true;
            ElFiltro.Filtrado = true;
            Exportar = true;
        }
        
        protected void LeerConstantes()
        {
            string[] NomMpios = Constantes.MpiosTodos.Split(",");
            for (int i = 0; i < NomMpios.Length; i++)
            {
                LosMpios.Add(NomMpios[i]);
            }
            
            string[] tiposTemp = Constantes.SolicitudTipo.Split(",");
            foreach (var tipo in tiposTemp)
            {
                LosTipos.Add(tipo);
            }
            LosEdos.Add(new KeyValuePair<int, string>(1, "Entregado"));
            LosEdos.Add(new KeyValuePair<int, string>(2, "Proceso"));
            LosEdos.Add(new KeyValuePair<int, string>(3, "Cancelada"));
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

        [Inject]
        public I110UsuariosServ UsersIServ { get; set; } = default!;
        [Parameter]
        public Z110_Usuarios ElUsuario { get; set; } = new();
        public IEnumerable<Z110_Usuarios> LosUsers { get; set; } =
            Enumerable.Empty<Z110_Usuarios>();
        [Inject]
        public NavigationManager NM { get; set; } = default!;
        public async Task LeerUser()
        {
            var autState = await AuthStateTask;
            var user = autState.User;
            if (!user.Identity!.IsAuthenticated) NM.NavigateTo("/firma?laurl=/inicio");
            UserIdLogAll = user.FindFirst(c => c.Type == "sub")?.Value!;

            LosUsers = await UsersIServ.Buscar("All", "Vacio");

            //var UserList = await UsersIServ.Buscar($"UserId_-_UserId_-_{UserIdLogAll}", "vacio");
            ElUsuario = LosUsers.FirstOrDefault(x=>x.UsuariosId == UserIdLogAll)!;
            if (ElUsuario == null) NM.NavigateTo("/inicio");
        }
        
        public List<ReporteDet> LlenadoReporte(IEnumerable<Z232_DetSol> datos)
        {
            List<ReporteDet> resultado = new List<ReporteDet>();
            if (datos.Any())
            {
                foreach (var reg in datos)
                {
                    ReporteDet nuevo = new();
                    nuevo.FolioRt = reg.Folio;
                    nuevo.EstadoR = reg.Estado;
                    nuevo.EstadoRt = LosEdos.FirstOrDefault(x => 
                                        x.Key == reg.Estado).Value;
                    nuevo.AlmacenR = Consulta(reg.SolicitudId, "AlmacenR");
                    nuevo.AlmacenRt = Consulta(nuevo.AlmacenR, "AlmacenRt");
                    nuevo.TipoEntradaRt = Consulta(reg.SolicitudId, "Tipo");
                    nuevo.ComercioR = Consulta(reg.SolicitudId, "ComercioR");               
                    nuevo.ComercioRt = Consulta(reg.SolicitudId, "ComercioRt");
                    nuevo.ProductoR = reg.Producto;
                    nuevo.ProductoRt = Consulta(reg.Producto, "Producto");
                    nuevo.CiudadRt = Consulta(reg.SolicitudId, "Ciudad");
                    nuevo.CantidadRt = reg.Cantidad;

                    resultado.Add(nuevo);
                }
            }
            return resultado;
        }
        public Dictionary<string, string> DatosDic { get; set; } = new Dictionary<string, string>();
        protected string Consulta(string valor, string tipo)
        {
            string respuesta = string.Empty;
            if (DatosDic.ContainsKey($"{tipo}_{valor}")) 
                return DatosDic[$"{tipo}_{valor}"];
            switch (tipo)
            {
                case "AlmacenR":
                    respuesta += LasSolicitudes.FirstOrDefault(x =>
                                x.SolicitudId == valor)!.Almacen;
                    respuesta = !string.IsNullOrEmpty(respuesta) ? respuesta : "SIN INFO Almacen";
                    break;
                case "AlmacenRt":
                    respuesta += LosAlmacenes.FirstOrDefault(x =>
                                x.AlmacenId == valor)!.Corto;
                    respuesta = !string.IsNullOrEmpty(respuesta) ? respuesta : "SIN INFO Almacen";
                    break;
                case "Tipo":
                    respuesta += LasSolicitudes.FirstOrDefault(x =>
                                x.SolicitudId == valor)!.Tipo;
                    respuesta = !string.IsNullOrEmpty(respuesta) ? respuesta : "SIN INFO tipo";
                    break;
                case "ComercioR":
                    respuesta += LasSolicitudes.FirstOrDefault(x =>
                                x.SolicitudId == valor)!.Usuario;
                    respuesta = !string.IsNullOrEmpty(respuesta) ? respuesta : "SIN INFO User";
                    break;

                case "ComercioRt":
                    var luser = LasSolicitudes.FirstOrDefault(x =>
                                x.SolicitudId == valor)!.Usuario;
                    respuesta += LosUsers.FirstOrDefault(x =>
                                x.UsuariosId == luser)!.OldEmail;
                    respuesta = !string.IsNullOrEmpty(respuesta) ? respuesta : "SIN INFO User";
                    break;
                case "Producto":
                    respuesta += LosProductos.FirstOrDefault(x => 
                                x.ProductoId == valor)!.Corto;
                    respuesta += LosProductos.FirstOrDefault(x =>
                                x.ProductoId == valor)!.Nombre;
                    respuesta = !string.IsNullOrEmpty(respuesta) ? respuesta : "Sin Producto";

                    break;
                case "Ciudad":
                    var luserC = LasSolicitudes.FirstOrDefault(x =>
                                x.SolicitudId == valor)!.Usuario;
                    respuesta += LosUsers.FirstOrDefault(x =>
                                x.UsuariosId == luserC)!.Municipio;
                    respuesta = !string.IsNullOrEmpty(respuesta) ? respuesta : "Hermosillo";
                    break;

                default:
                    break;
            }
            DatosDic.Add($"{tipo}_{valor}", respuesta);
            return respuesta;

        }
        public class Filtro
        {
            public string FolioF { get; set; } = string.Empty;
            public int EstadoF { get; set; } = 0;
            public string AlmacenF { get; set; } = "Alla";
            public string TipoEntradaF { get; set; } = "Alla";
            public string ComercioF { get; set; } = "Alla";
            public string ProductoF { get; set; } = "Alla";
            public string CiudadF { get; set; } = "Alla";
            public int CantidadF { get; set; } = 0;
            public bool Filtrado { get; set; } = false;
        }

        public class ReporteDet
        {
            public string FolioRt { get; set; } = string.Empty;
            public int EstadoR { get; set; } = 0;
            public string EstadoRt { get; set; } = string.Empty;
            public string AlmacenR { get; set; } = string.Empty;
            public string AlmacenRt { get; set; } = string.Empty;
            public string TipoEntradaRt { get; set; } = string.Empty;
            public string ComercioR { get; set; } = string.Empty;
            public string ComercioRt { get; set; } = string.Empty;
            public string ProductoR { get; set; } = string.Empty;
            public string ProductoRt { get; set; } = string.Empty;
            public string CiudadRt { get; set; } = string.Empty;
            public int CantidadRt { get; set; } = 0;
        }
    }
}

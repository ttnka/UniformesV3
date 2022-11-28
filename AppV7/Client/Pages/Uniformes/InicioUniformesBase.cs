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
        public double showavance { get; set; } = 0;
        public string TxtMetaGral { get; set; } = string.Empty;
        public double DataAlmacenes { get; set; } = 0;
        public string DataAlmacenesName { get; set; } = string.Empty;
        public double DataComercios { get; set; } = 0;
        public string TxtDataComercios { get; set; } = string.Empty;
        public double DataFoliosHillo { get; set; } = 0;
        public string TxtDataFoliosHillo { get; set; } = string.Empty;
        public double DataFoliosGuaymas { get; set; } = 0;
        public double DataFoliosNogales { get; set; } = 0;
        public string TxtDataFoliosGuaymas { get; set; } = string.Empty;
        public string TxtDataFoliosNogales { get; set; } = string.Empty;
        public string TxtDataAlmComer { get; set; } = string.Empty;
        public string TxtMpios { get; set; } = string.Empty;
        public Dictionary<string, string> Graficas { get; set; } = 
            new Dictionary<string, string>();

        protected override async Task OnInitializedAsync()
        {
            await LeerGraficos();
            showRenglon = MyFunc.DameRandom(1, 100);
            //showavance = MyFunc.DameRandom(1, 100);
            Console.WriteLine(showavance);
        }
        protected async Task LeerGraficos()
        {
            // General  Folios Entregados /  Meta GENERAL 

            LosFolios = await FoliosIServ.Buscar("Alla");
            LasMetas = await MetasIServ.Buscar("Alla");
            var General = LasMetas.FirstOrDefault(x => x.Titulo == "GENERAL").Cantidad;
            if (General < 1) General = 1;
            TxtMetaGral = General.ToString();
            showavance = ((100) * (LosFolios.Count()) / General) < 1 ? 1 : 
                ((100) * (LosFolios.Count()) / General);
            
            // Almacenes y Comercioss sumatoria de los 2
            LosAlmacenes = await AlmacenIServ.Buscar("Alla");
            DataAlmacenes = LosAlmacenes.Count() == 0 ? 1 : LosAlmacenes.Count();
            
            LosComerciantes = await UsersIServ.Buscar("All", "Vacio");
            DataComercios = LosComerciantes.Count(x => x.Nivel == 2) == 0 ?
                                1 : LosComerciantes.Count(x => x.Nivel == 2);

            TxtDataAlmComer = $"{DataAlmacenes},{DataComercios}";
            
            // Folios entregados en Guyamas y Hermosillo

            DataFoliosNogales = LosFolios.Count(x => x.Localidad == "Nogales") == 0 ?
                1 : LosFolios.Count(x => x.Localidad == "Nogales");
            TxtDataFoliosNogales = DataFoliosNogales.ToString();

            DataFoliosHillo = LosFolios.Count(x => x.Localidad == "Hermosillo") == 0 ?
                1 : LosFolios.Count(x => x.Localidad == "Hermosillo");
            TxtDataFoliosHillo = DataFoliosHillo.ToString();

            TxtMpios = $"{TxtDataFoliosHillo},{TxtDataFoliosNogales}";
            
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
        public I110UsuariosServ UsersIServ { get; set; } = default!;
        [Parameter]
        public Z110_Usuarios ElUsuarioAll { get; set; } = new();

        [Inject]
        public I290MetaServ MetasIServ { get; set; } = default!;
        public IEnumerable<Z290_Meta> LasMetas { get; set; } = new List<Z290_Meta>();
        [Inject]
        public I260FolioServ FoliosIServ { get; set; } = default!;
        public IEnumerable<Z260_Folio> LosFolios { get; set; } = new List<Z260_Folio>();
        [Inject]
        public I210AlmacenServ AlmacenIServ { get; set; } = default!;
        public IEnumerable<Z210_Almacen> LosAlmacenes { get; set; } = new List<Z210_Almacen>();
        public IEnumerable<Z110_Usuarios> LosComerciantes { get; set; } = new List<Z110_Usuarios>();

    }
}

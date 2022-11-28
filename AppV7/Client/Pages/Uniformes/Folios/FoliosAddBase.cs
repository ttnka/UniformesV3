using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using CsvHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Win32;
using Radzen;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Globalization;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AppV7.Client.Pages.Uniformes.Folios
{
    public class FoliosAddBase : ComponentBase 
    {
        [Inject]
        public I260FolioServ FolioIServ { get; set; } = default!;
        protected async override Task OnInitializedAsync()
        {
            await LeerUser();
        }
        [Inject]
        public HttpClient Http { get; set; } = default!;

        private int Titulos = 0;
        private int Vacios = 0;
        private List<Z260_Folio> ResultList = new();

        private int MaxAllowedFiles = 3;
        private long MaxFileSize = long.MaxValue;
        [Parameter]
        public List<string> FileNames { get; set; } = new();
        public string FullName = string.Empty;
        private List<Z812_Upload> upLoadResults = new();
        
        private Z260_Folio PoblandoZ260(string[] v) 
        {
            string texto = "v3= " + v[3] + " v4= " + v[4] + " v5= " + v[5] + " v6= " + v[6] + " v7= " + v[7] + " v9= " + v[9];
            texto += "v11=" + v[11] + " v12= " + v[12] + " v14= " + v[14] + " v15= " + v[15] + v[17] + v[19] + v[21] + v[22];
            Console.WriteLine(texto);
            Z260_Folio resultado = new();
            
            resultado.FolioId = Guid.NewGuid().ToString();
            resultado.RegId = v[0];
            resultado.FechaEntrega = Convert.ToDateTime(v[2]);
            resultado.Status = v[14];
            resultado.Folio = v[4];
            resultado.NombreCompleto = v[5];
            resultado.Curp = v[6];
            resultado.TurnoId = v[7];
            resultado.Grado = v[9];
            resultado.Codigo = v[11];
            resultado.EscuelaId = v[12];
            resultado.InscStatusId = v[14];
            resultado.GeneroId = v[15];
            resultado.NivelId = v[17];
            resultado.TipoValId = v[19];
            resultado.Localidad = v[21];
            resultado.Municipio = v[22];      
            return resultado;
        }
        private async Task AddFoliosDb(string tabla, List<string[]> csv)
        {   
            switch (tabla){
                case "Folio":
                    foreach(var reg in csv)
                    {  
                        if (string.IsNullOrEmpty(reg[0]) || reg[0] == "Fin" ||
                            string.IsNullOrEmpty(reg[4]))
                        { 
                            continue; 
                        }
                        else
                        {
                            var radd = PoblandoZ260(reg);
                            await FolioIServ.AddFolio(radd);
                            //ResultList.Add(radd);
                        }
                    }
                break;

                default:

                    break;
            }
            //await FolioIServ.AddFoliosVarios(ResultList);
        }
        public async Task OIFChange(InputFileChangeEventArgs arg)
        {
            foreach (var file in arg.GetMultipleFiles(MaxAllowedFiles))
            {
                Titulos = 0;
                var lector = file.OpenReadStream();
                var csv = new List<string[]>();
                MemoryStream ms = new MemoryStream();
                await lector.CopyToAsync(ms);
                lector.Close();
                var outputFileString = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                foreach (var item in outputFileString.Split(Environment.NewLine))
                {
                    if (Titulos != 0)
                        csv.Add(SplitCSV(item.ToString()));
                    Titulos++;
                }
                Vacios = 0;
                await AddFoliosDb("Folio", csv);
            }
            
           
            
    
    #region SUBIR Archivo
            using var content = new MultipartFormDataContent();
            foreach (var file in arg.GetMultipleFiles(MaxAllowedFiles))
            {
                var fileCont = new StreamContent(file.OpenReadStream(MaxFileSize));
                fileCont.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                FullName = string.Empty;
                FullName = BuildFull(file.Name);
                FileNames.Add(FullName);
                content.Add(
                    content: fileCont,
                    name: "\"files\"",
                    fileName: FullName);
            }

            var respuesta = await Http.PostAsync("/api/Files", content);
            //var respuesta = await Http.PostAsync("/api/C812Files", content);
    #endregion
        }

        private string[] SplitCSV(string input)
        {
            //Excludes commas within quotes  
            Regex csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", 
                RegexOptions.Compiled);
            List<string> list = new List<string>();
            string curr = null;
            foreach (Match match in csvSplit.Matches(input))
            {
                curr = match.Value;
                if (0 == curr.Length) list.Add("");
                list.Add(curr.TrimStart(','));
            }
            return list.ToArray();
        }
        protected string? GetStoredFiles(string fileName)
        {
            var upLoadResult = upLoadResults.SingleOrDefault(f => f.FileName == fileName);
            return upLoadResult is not null ? upLoadResult.StoredName : "Archivo No encontrado";
        }
        public string BuildFull(string name)
        {
            Random random = new Random();
            string resultado = string.Empty;
            resultado += DateTime.Now.Year.ToString();
            resultado += (DateTime.Now.Month < 10 ? "0" : "") + DateTime.Now.Month.ToString();
            resultado += (DateTime.Now.Day < 10 ? "0" : "") + DateTime.Now.Day.ToString();
            resultado += (DateTime.Now.Hour < 10 ? "0" : "") + DateTime.Now.Hour.ToString();
            resultado += (DateTime.Now.Minute < 10 ? "0" : "") + DateTime.Now.Minute.ToString();
            resultado += random.Next(10, 100).ToString();
            //resultado += (DateTime.Now.Second < 10 ? "0" : "") + DateTime.Now.Second.ToString();
            resultado += $"_{name}";
            return resultado;
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
        [Inject]
        public NavigationManager NM { get; set; } = default!;
        public async Task LeerUser()
        {
            var autState = await AuthStateTask;
            var user = autState.User;
            if (!user.Identity!.IsAuthenticated) NM.NavigateTo("/firma?laurl=/inicio");
            UserIdLogAll = user.FindFirst(c => c.Type == "sub")?.Value!;
            var UserList = await UsersIServ.Buscar($"UserId_-_UserId_-_{UserIdLogAll}", "vacio");
            ElUsuario = UserList.FirstOrDefault()!;
        }

    }
}

using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen.Blazor;
using System;
using static System.Net.WebRequestMethods;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace AppV7.Client.Pages.LaWeb
{
    public class FilesListBase : ComponentBase
    {
        [Inject]
        public I810GeneralServ GeneralIServ { get; set; } = default!;
        
        [Inject]
        public I812FilesServ FileIServ { get; set; } = default!;
        public Z812_Files ElFile  { get; set; } = new Z812_Files();
        public RadzenUpload? UploadForm { get; set; } = default!;
        [Parameter]
        public Z810_General Registro { get; set; } = new();
        public bool Mostrar { get; set; } = true;
        public class Tipos
        {
            public string ElId { get; set; } = null!;
            public string ElTipo { get; set; } = null!;
        }

        public List<Tipos> TiposList = new List<Tipos>()
        {
        new Tipos() { ElId = "Principal", ElTipo= "Principal" },
        new Tipos() { ElId =  "Carrucel", ElTipo= "Carrucel" }
        
        };

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

        [Inject]
        public HttpClient Http { get; set; } = default!;

        private int MaxAllowedFiles = int.MaxValue;
        private long MaxFileSize = long.MaxValue;
        public List<string> FileNames = new();
        public string FullName = string.Empty;
        private List<Z812_Upload> upLoadResults = new();
        public async Task OIFChange(InputFileChangeEventArgs arg)
        {
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

            //var respuesta = await Http.PostAsync("/api/Files", content);
            var respuesta = await Http.PostAsync("/api/C812Files", content);
            var newUploadResults = await respuesta.Content.ReadFromJsonAsync<List<Z812_Upload>>();
            if (newUploadResults is not null)
            {
                upLoadResults = upLoadResults.Concat(newUploadResults).ToList();
                foreach (var result in upLoadResults)
                {
                    ElFile.Archivo = result.StoredName!;
                    ElFile.Org = ElUsuario.OrgId;
                    ElFile.FuenteId = Registro.GeneralId;
                    ElFile.Fuente = Registro.Titulo;

                    ElFile.FileId = Guid.NewGuid().ToString();
                    ElFile.Fecha = DateTime.Now;
                    ElFile.Tipo = "Tipo";
                    ElFile.Gpo = "News";
                    ElFile.Estado = 1;   
                    await FileIServ.AddFile(ElFile);
                }
              
            }
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

    }
}

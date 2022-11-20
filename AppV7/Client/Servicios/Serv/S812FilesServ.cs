using AppV7.Client.Pages.Admin;
using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;

namespace AppV7.Client.Servicios.Serv
{
    public class S812FilesServ : I812FilesServ
    {
        private readonly HttpClient _httpClient;

        public S812FilesServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Z812_Files> AddFile(Z812_Files files)
        {
            var newFiles = await _httpClient.PostAsJsonAsync<Z812_Files>("/api/C812Files/", files);
            if (newFiles.IsSuccessStatusCode)
            {
                return await newFiles.Content.ReadFromJsonAsync<Z812_Files>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Z812_Files>> Buscar(string clave)
        {
            var resultado = $"/api/C812Files/filtro?clave={clave}";
            return await _httpClient.GetFromJsonAsync<IEnumerable<Z812_Files>>(resultado);
        }

        public async Task<Z812_Files> UpDateFile(Z812_Files files)
        {
            var UpdateFiles = await _httpClient.PutAsJsonAsync<Z812_Files>("/api/C812Files", files);
            if (UpdateFiles.IsSuccessStatusCode)
            {
                return await UpdateFiles.Content.ReadFromJsonAsync<Z812_Files>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Z812_Upload>> UploadFiles(MultipartFormDataContent filesList)
        {
            var newUpload = await _httpClient.PostAsync("/api/C812Files/upload/", filesList);
            //var newUpload = await _httpClient.PostAsJsonAsync<List<IFormFile>("/api/C812Files/upload/", filesList);
            if (newUpload.IsSuccessStatusCode)
            {
                return await newUpload.Content.ReadFromJsonAsync<IEnumerable<Z812_Upload>>();
            }
            else
            {
                return null;
            }
        }
    }
}

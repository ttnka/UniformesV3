using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using System.Net.Http.Json;

namespace AppV7.Client.Servicios.Serv
{
    public class S800WebSiteServ : I800WebSiteServ
    {
        private readonly HttpClient _httpClient;

        public S800WebSiteServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Z800_WebSite> AddWebSite(Z800_WebSite webSite)
        {
            var newWebSite = await _httpClient.PostAsJsonAsync<Z800_WebSite>("/api/C800WebSite/", webSite);
            if (newWebSite.IsSuccessStatusCode)
            {
                return await newWebSite.Content.ReadFromJsonAsync<Z800_WebSite>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Z800_WebSite>> Buscar(string clave)
        {
            var resultado = $"/api/C800WebSites/filtro?clave={clave}";

            return await _httpClient.GetFromJsonAsync<IEnumerable<Z800_WebSite>>(resultado);
        }

        public async Task<Z800_WebSite> UpdateWebSite(Z800_WebSite webSite)
        {
            var UpdatewebSite = await _httpClient.PutAsJsonAsync<Z800_WebSite>("/api/C800WebSites", webSite);
            if (UpdatewebSite.IsSuccessStatusCode)
            {
                return await UpdatewebSite.Content.ReadFromJsonAsync<Z800_WebSite>();
            }
            else
            {
                return null;
            }
        }
    }
}

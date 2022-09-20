using AppV7.Client.Pages.Admin;
using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using System.Net.Http.Json;

namespace AppV7.Client.Servicios.Serv
{
    public class S802WebConfigServ : I802WebConfigServ
    {
        private readonly HttpClient _httpClient;

        public S802WebConfigServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Z802_WebConfig> AddWebConfig(Z802_WebConfig wConfig)
        {
            var newWConfig = await _httpClient.PostAsJsonAsync<Z802_WebConfig>("/api/C802WebConfig/", wConfig);
            if (newWConfig.IsSuccessStatusCode)
            {
                return await newWConfig.Content.ReadFromJsonAsync<Z802_WebConfig>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Z802_WebConfig>> Buscar(string clave)
        {
            var resultado = $"/api/C802WebConfig/filtro?clave={clave}";

            return await _httpClient.GetFromJsonAsync<IEnumerable<Z802_WebConfig>>(resultado);
        }

        public async Task<Z802_WebConfig> UpDateWebConfig(Z802_WebConfig wConfig)
        {
            var UpdawConfig = await _httpClient.PutAsJsonAsync<Z802_WebConfig>("/api/C802WebConfig/", wConfig);
            if (UpdawConfig.IsSuccessStatusCode)
            {
                return await UpdawConfig.Content.ReadFromJsonAsync<Z802_WebConfig>();
            }
            else
            {
                return null;
            }
        }
    }
}

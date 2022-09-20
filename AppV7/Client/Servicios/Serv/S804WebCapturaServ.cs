using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using System.Net.Http;
using System.Net.Http.Json;

namespace AppV7.Client.Servicios.Serv
{
    public class S804WebCapturaServ : I804WebCapturaServ
    {
        private readonly HttpClient _httpClient;

        public S804WebCapturaServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Z804_WebCaptura> AddWebCaptura(Z804_WebCaptura wCaptura)
        {
            var newWCaptura = await _httpClient.PostAsJsonAsync<Z804_WebCaptura>("/api/C804WebCaptura/", wCaptura);
            if (newWCaptura.IsSuccessStatusCode)
            {
                return await newWCaptura.Content.ReadFromJsonAsync<Z804_WebCaptura>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Z804_WebCaptura>> Buscar(string clave)
        {
            var resultado = $"/api/C804WebCaptura/filtro?clave={clave}";

            return await _httpClient.GetFromJsonAsync<IEnumerable<Z804_WebCaptura>>(resultado);
        }

        public async Task<Z804_WebCaptura> UpdateWebCaptura(Z804_WebCaptura wCaptura)
        {
            var UpdawConfig = await _httpClient.PutAsJsonAsync<Z804_WebCaptura>("/api/C804WebCaptura/", wCaptura);
            if (UpdawConfig.IsSuccessStatusCode)
            {
                return await UpdawConfig.Content.ReadFromJsonAsync<Z804_WebCaptura>();
            }
            else
            {
                return null;
            }
        }
    }
}

using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using System.Net.Http.Json;

namespace AppV7.Client.Servicios.Serv
{
    public class S204ContDetServ : I204ContDetServ
    {
        private readonly HttpClient _httpClient;

        public S204ContDetServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Z204_ContDet> AddContDet(Z204_ContDet contDet)
        {
            var newContacto = await _httpClient.PostAsJsonAsync<Z204_ContDet>("/api/C204ContDet/", contDet);
            if (newContacto.IsSuccessStatusCode)
            {
                return await newContacto.Content.ReadFromJsonAsync<Z204_ContDet>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Z204_ContDet>> Buscar(string clave)
        {
            var resultado = $"/api/C204ContDet/filtro?clave={clave}";

            return await _httpClient.GetFromJsonAsync<IEnumerable<Z204_ContDet>>(resultado);
        }

        public async Task<Z204_ContDet> UpdateContDet(Z204_ContDet contDet)
        {
            var UpdateContacto = await _httpClient.PutAsJsonAsync<Z204_ContDet>("/api/C204ContDet/", contDet);
            if (UpdateContacto.IsSuccessStatusCode)
            {
                return await UpdateContacto.Content.ReadFromJsonAsync<Z204_ContDet>();
            }
            else
            {
                return null;
            }
        }
    }
}

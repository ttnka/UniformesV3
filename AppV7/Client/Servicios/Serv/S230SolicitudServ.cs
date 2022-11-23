using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using System.Net.Http;
using System.Net.Http.Json;

namespace AppV7.Client.Servicios.Serv
{
    public class S230SolicitudServ : I230SolicitudServ
    {
        private readonly HttpClient _httpClient;

        public S230SolicitudServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Z230_Solicitud> AddSolicitud(Z230_Solicitud solicitud)
        {
            var newSol = await _httpClient.PostAsJsonAsync<Z230_Solicitud>("/api/C230Solicitud/", solicitud);
            if (newSol.IsSuccessStatusCode)
            {
                return await newSol.Content.ReadFromJsonAsync<Z230_Solicitud>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Z230_Solicitud>> Buscar(string clave)
        {
            var resultado = $"/api/C230Solicitud/filtro?clave={clave}";
            return await _httpClient.GetFromJsonAsync<IEnumerable<Z230_Solicitud>>(resultado);
        }

        public async Task<Z230_Solicitud> UpDateSolicitud(Z230_Solicitud solicitud)
        {
            var updateSol = await _httpClient.PutAsJsonAsync<Z230_Solicitud>("/api/C230Solicitud/", solicitud);
            return updateSol.IsSuccessStatusCode ?
                await updateSol.Content.ReadFromJsonAsync<Z230_Solicitud>() : null;
        }
    }
}

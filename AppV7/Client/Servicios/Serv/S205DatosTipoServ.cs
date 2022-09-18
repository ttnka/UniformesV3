using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using System.Net.Http.Json;

namespace AppV7.Client.Servicios.Serv
{
    public class S205DatosTipoServ : I205DatosTipoServ
    {
        private readonly HttpClient _httpClient;

        public S205DatosTipoServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Z205_DatosTipo> AddDatosTipo(Z205_DatosTipo datosTipo)
        {
            var newDatosTipo = await _httpClient.PostAsJsonAsync<Z205_DatosTipo>("/api/C205DatosTipo/", datosTipo);
            if (newDatosTipo.IsSuccessStatusCode)
            {
                return await newDatosTipo.Content.ReadFromJsonAsync<Z205_DatosTipo>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Z205_DatosTipo>> Buscar(string clave)
        {
            var resultado = $"/api/C205datosTipo/filtro?clave={clave}";

            return await _httpClient.GetFromJsonAsync<IEnumerable<Z205_DatosTipo>>(resultado);
        }

        public async Task<Z205_DatosTipo> UpdateDatosTipo(Z205_DatosTipo datosTipo)
        {
            var UpdateDatosTipo = await _httpClient.PutAsJsonAsync<Z205_DatosTipo>("/api/C205DatosTipo/", datosTipo);
            if (UpdateDatosTipo.IsSuccessStatusCode)
            {
                return await UpdateDatosTipo.Content.ReadFromJsonAsync<Z205_DatosTipo>();
            }
            else
            {
                return null;

            }
        }
    }
}

using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using System.Net.Http.Json;

namespace AppV7.Client.Servicios.Serv
{
    public class S810GeneralServ : I810GeneralServ
    {
        private readonly HttpClient _httpClient;

        public S810GeneralServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Z810_General> AddGeneral(Z810_General gral)
        {
            var newGral = await _httpClient.PostAsJsonAsync<Z810_General>("/api/C810General/", gral);
            if (newGral.IsSuccessStatusCode)
            {
                return await newGral.Content.ReadFromJsonAsync<Z810_General>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Z810_General>> Buscar(string clave)
        {
            var resultado = $"/api/C810General/filtro?clave={clave}";

            return await _httpClient.GetFromJsonAsync<IEnumerable<Z810_General>>(resultado);
        }

        public async Task<Z810_General> UpDateGeneral(Z810_General gral)
        {
            var UpdateGral = await _httpClient.PutAsJsonAsync<Z810_General>("/api/C810General/", gral);
            return UpdateGral.IsSuccessStatusCode ?
                await UpdateGral.Content.ReadFromJsonAsync<Z810_General>() : null;
        }
    }
}

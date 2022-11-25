using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using System.Net.Http.Json;

namespace AppV7.Client.Servicios.Serv
{
    public class S260FolioServ : I260FolioServ
    {
        private readonly HttpClient _httpClient;

        public S260FolioServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Z260_Folio> AddFolio(Z260_Folio folio)
        {
            var newFolio = await _httpClient.PostAsJsonAsync<Z260_Folio>("/api/C260folio/", folio);
            if (newFolio.IsSuccessStatusCode)
            {
                return await newFolio.Content.ReadFromJsonAsync<Z260_Folio>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Z260_Folio>> Buscar(string clave)
        {
            var resultado = $"/api/C260Folio/filtro?clave={clave}";
            return await _httpClient.GetFromJsonAsync<IEnumerable<Z260_Folio>>(resultado);
        }

        public async Task<Z260_Folio> UpDateFolio(Z260_Folio folio)
        {
            var UpdateFolio = await _httpClient.PutAsJsonAsync<Z260_Folio>("/api/C260Folio/", folio);
            return UpdateFolio.IsSuccessStatusCode ?
                await UpdateFolio.Content.ReadFromJsonAsync<Z260_Folio>() : null;
        }
    }
}

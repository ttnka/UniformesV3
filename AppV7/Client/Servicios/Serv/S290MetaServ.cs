using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using System.Net.Http.Json;

namespace AppV7.Client.Servicios.Serv
{
    public class S290MetaServ : I290MetaServ
    {
        private readonly HttpClient _httpClient;

        public S290MetaServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Z290_Meta> AddMeta(Z290_Meta meta)
        {
            var newMeta = await _httpClient.PostAsJsonAsync<Z290_Meta>("/api/C290Meta/", meta);
            if (newMeta.IsSuccessStatusCode)
            {
                return await newMeta.Content.ReadFromJsonAsync<Z290_Meta>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Z290_Meta>> Buscar(string clave)
        {
            var resultado = $"/api/C290Meta/filtro?clave={clave}";
            return await _httpClient.GetFromJsonAsync<IEnumerable<Z290_Meta>>(resultado);
        }

        public async Task<Z290_Meta> UpDateMeta(Z290_Meta meta)
        {
            var UpdateMeta = await _httpClient.PutAsJsonAsync<Z290_Meta>("/api/C290Meta/", meta);
            return UpdateMeta.IsSuccessStatusCode ?
                await UpdateMeta.Content.ReadFromJsonAsync<Z290_Meta>() : null;
        }
    }
}

using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using System.Net.Http.Json;

namespace AppV7.Client.Servicios.Serv
{
    public class S100OrgServ : I100OrgServ 
    {
        private readonly HttpClient _httpClient;

        public S100OrgServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Z100_Org> AddOrg(Z100_Org org)
        {
            var newOrg = await _httpClient.PostAsJsonAsync<Z100_Org>("/api/C100Org/", org);
            if (newOrg.IsSuccessStatusCode)
            {
                return await newOrg.Content.ReadFromJsonAsync<Z100_Org>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Z100_Org>> Buscar(string clave)
        {
            var resultado = $"/api/C100Org/filtro?clave={clave}";

            return await _httpClient.GetFromJsonAsync<IEnumerable<Z100_Org>>(resultado);
        }

        public async Task<Z100_Org> UpDateOrg(Z100_Org org)
        {
            var UpdateOrg = await _httpClient.PutAsJsonAsync<Z100_Org>("/api/C100Org/", org);
            return UpdateOrg.IsSuccessStatusCode ?
                await UpdateOrg.Content.ReadFromJsonAsync<Z100_Org>() : null;
        }
    }
}

using AppV7.Client.Servicios.IFaceServ;
using System.Net.Http.Json;

namespace AppV7.Client.Servicios.Serv
{
    public class SRegistroInicialServ : IRegistroInicialServ
    {
        private readonly HttpClient _httpClient;

        public SRegistroInicialServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DoRegInicial()
        {
            var newOrg = await _httpClient.PostAsJsonAsync<bool>("/api/CRegIni/", true);
            return newOrg.IsSuccessStatusCode;
        }
    }
}

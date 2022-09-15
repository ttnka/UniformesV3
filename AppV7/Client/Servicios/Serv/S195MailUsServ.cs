using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using System.Net.Http.Json;

namespace AppV7.Client.Servicios.Serv
{
    public class S195MailUsServ : I195MailUsServ
    {
        private readonly HttpClient _httpClient;

        public S195MailUsServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Z195_MailUs> AddMailUs(Z195_MailUs mailUs)
        {
            var newMailUs = await _httpClient.PostAsJsonAsync<Z195_MailUs>("/api/C195MailUs/", mailUs);
            if (newMailUs.IsSuccessStatusCode)
            {
                return await newMailUs.Content.ReadFromJsonAsync<Z195_MailUs>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Z195_MailUs>> Buscar(string clave)
        {
            var resultado = $"/api/C195MailUs/filtro?clave={clave}";

            return await _httpClient.GetFromJsonAsync<IEnumerable<Z195_MailUs>>(resultado);
        }
    }
}

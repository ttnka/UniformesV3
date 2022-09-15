using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using System.Net.Http.Json;

namespace AppV7.Client.Servicios.Serv
{
    public class S840ContactanosServ : I840ContactanosServ 
    {
        private readonly HttpClient _httpClient;

        public S840ContactanosServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Z840_Contactanos> AddContactanos(Z840_Contactanos contactanos)
        {
            var newUsuario = await _httpClient.PostAsJsonAsync<Z840_Contactanos>("/api/C840Contactanos/", contactanos);
            if (newUsuario.IsSuccessStatusCode)
            {
                return await newUsuario.Content.ReadFromJsonAsync<Z840_Contactanos>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Z840_Contactanos>> Buscar(string clave)
        {
            var resultado = $"/api/C840Contactanos/filtro?clave={clave}";

            return await _httpClient.GetFromJsonAsync<IEnumerable<Z840_Contactanos>>(resultado);
        }

        public async Task<Z840_Contactanos> UpdateContactanos(Z840_Contactanos contactanos)
        {
            var UpdaUsuario = await _httpClient.PutAsJsonAsync<Z840_Contactanos>("/api/C840Contactanos/", contactanos);
            if (UpdaUsuario.IsSuccessStatusCode)
            {
                return await UpdaUsuario.Content.ReadFromJsonAsync<Z840_Contactanos>();
            }
            else
            {
                return null;
            }
        }
    }
}

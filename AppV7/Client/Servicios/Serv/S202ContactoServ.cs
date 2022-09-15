using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using System.Net.Http.Json;

namespace AppV7.Client.Servicios.Serv
{
    public class S202ContactoServ : I202ContactoServ
    {
        private readonly HttpClient _httpClient;

        public S202ContactoServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Z202_Contacto> AddContacto(Z202_Contacto contacto)
        {
            var newContacto = await _httpClient.PostAsJsonAsync<Z202_Contacto>("/api/C202Contacto/", contacto);
            if (newContacto.IsSuccessStatusCode)
            {
                return await newContacto.Content.ReadFromJsonAsync<Z202_Contacto>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Z202_Contacto>> Buscar(string clave)
        {
            var resultado = $"/api/C202Contacto/filtro?clave={clave}";

            return await _httpClient.GetFromJsonAsync<IEnumerable<Z202_Contacto>>(resultado);
        }

        public async Task<Z202_Contacto> UpdateContacto(Z202_Contacto contacto)
        {
            var UpdateContacto = await _httpClient.PutAsJsonAsync<Z202_Contacto>("/api/C202Contacto/", contacto);
            if (UpdateContacto.IsSuccessStatusCode)
            {
                return await UpdateContacto.Content.ReadFromJsonAsync<Z202_Contacto>();
            }
            else
            {
                return null;
            }
        }
    }
}

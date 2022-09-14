using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using System.Net.Http.Json;

namespace AppV7.Client.Servicios.Serv
{
    public class SAddUsuarioServ : IAddUsuarioServ 
    {
        private readonly HttpClient _httpClient;

        public SAddUsuarioServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<EAddUsuario> AddUsuario(EAddUsuario addUsuario)
        {
            var newAU = await _httpClient.PostAsJsonAsync<EAddUsuario>("/api/addusuario/", addUsuario);
            if (newAU.IsSuccessStatusCode)
            {
                return await newAU.Content.ReadFromJsonAsync<EAddUsuario>();
            }
            else
            {
                return null;
            }
        }
        public async Task<ERecupera> Recupera(ERecupera recupera)
        {
            var newAU = await _httpClient.PostAsJsonAsync<ERecupera>("/api/addusuario/recupera/", recupera);
            if (newAU.IsSuccessStatusCode)
            {
                return await newAU.Content.ReadFromJsonAsync<ERecupera>();
            }
            else
            {
                return null;
            }
        }
        public async Task<EAddUsuario> ResetPassword(EAddUsuario eAddUsuario)
        {
            var newAU = await _httpClient.PostAsJsonAsync<EAddUsuario>("/api/addusuario/reset/", eAddUsuario);
            if (newAU.IsSuccessStatusCode)
            {
                return await newAU.Content.ReadFromJsonAsync<EAddUsuario>();
            }
            else
            {
                return null;
            }
        }
    }
}

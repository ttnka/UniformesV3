using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using System.Net.Http.Json;

namespace AppV7.Client.Servicios.Serv
{
    public class S110UsuariosServ : I110UsuariosServ 
    {
        private readonly HttpClient _httpClient;

        public S110UsuariosServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        string path = "/api/C110Usuarios/";
        public async Task<Z110_Usuarios> AddUsuario(Z110_Usuarios usuario)
        {
            var newUsuario = await _httpClient.PostAsJsonAsync<Z110_Usuarios>(path, usuario);
            if (newUsuario.IsSuccessStatusCode)
            {
                return await newUsuario.Content.ReadFromJsonAsync<Z110_Usuarios>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Z110_Usuarios>> Buscar(string clave, string orgX)
        {
            var resultado = $"{path}filtro?clave={clave}&orgX={orgX}";

            return await _httpClient.GetFromJsonAsync<IEnumerable<Z110_Usuarios>>(resultado);
        }

        public async Task<Z110_Usuarios> UpDateUsuario(Z110_Usuarios usuario)
        {
            var UpdaUsuario = await _httpClient.PutAsJsonAsync<Z110_Usuarios>(path, usuario);
            if (UpdaUsuario.IsSuccessStatusCode)
            {
                return await UpdaUsuario.Content.ReadFromJsonAsync<Z110_Usuarios>();
            }
            else
            {
                return null;
            }
            /*
            return UpdaUsuario.IsSuccessStatusCode ?
                await UpdaUsuario.Content.ReadFromJsonAsync<Z110_Usuarios>() : null;
            */
        }
    }
}

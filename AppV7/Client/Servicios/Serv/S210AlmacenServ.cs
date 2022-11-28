using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using System.Net.Http.Json;

namespace AppV7.Client.Servicios.Serv
{
    public class S210AlmacenServ : I210AlmacenServ
    {
        private readonly HttpClient _httpClient;

        public S210AlmacenServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        string path = "/api/C210Almacen/";
        public async Task<Z210_Almacen> AddAlmacen(Z210_Almacen almacen)
        {
            var newAlmacen = await _httpClient.PostAsJsonAsync<Z210_Almacen>(path, almacen);
            if (newAlmacen.IsSuccessStatusCode)
            {
                return await newAlmacen.Content.ReadFromJsonAsync<Z210_Almacen>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Z210_Almacen>> Buscar(string clave)
        {
            var resultado = $"{path}filtro?clave={clave}";
            return await _httpClient.GetFromJsonAsync<IEnumerable<Z210_Almacen>>(resultado);
        }

        public async Task<Z210_Almacen> UpDateAlmacen(Z210_Almacen almacen)
        {
            var updateAlmacen = await _httpClient.PutAsJsonAsync<Z210_Almacen>(path, almacen);
            return updateAlmacen.IsSuccessStatusCode ?
                await updateAlmacen.Content.ReadFromJsonAsync<Z210_Almacen>() : null;
        }
    }
}

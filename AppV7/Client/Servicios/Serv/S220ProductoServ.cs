using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using System.Net.Http.Json;

namespace AppV7.Client.Servicios.Serv
{
    public class S220ProductoServ : I220ProductoServ
    {
        private readonly HttpClient _httpClient;

        public S220ProductoServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        string path = "/api/C220Productos/";
        public async Task<Z220_Producto> AddProducto(Z220_Producto producto)
        {
            var newProd = await _httpClient.PostAsJsonAsync<Z220_Producto>(path, producto);
            if (newProd.IsSuccessStatusCode)
            {
                return await newProd.Content.ReadFromJsonAsync<Z220_Producto>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Z220_Producto>> Buscar(string clave)
        {
            var resultado = $"{path}filtro?clave={clave}";
            return await _httpClient.GetFromJsonAsync<IEnumerable<Z220_Producto>>(resultado);
        }

        public async Task<Z220_Producto> UpDateProducto(Z220_Producto producto)
        {
            var updateProd = await _httpClient.PutAsJsonAsync<Z220_Producto>(path, producto);
            return updateProd.IsSuccessStatusCode ?
                await updateProd.Content.ReadFromJsonAsync<Z220_Producto>() : null;
        }
    }
}

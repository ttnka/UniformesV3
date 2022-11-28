using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using System.Net.Http.Json;

namespace AppV7.Client.Servicios.Serv
{
    public class S190BitacoraServ : I190BitacoraServ
    {
        private readonly HttpClient _httpClient;

        public S190BitacoraServ(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        string path = "/api/C190Bitacora/";
        public async Task<Z190_Bitacora> AddBitacora(Z190_Bitacora bitacora)
        {
            var newBitacora = await _httpClient.PostAsJsonAsync<Z190_Bitacora>(path, bitacora);
            if (newBitacora.IsSuccessStatusCode)
            {
                return await newBitacora.Content.ReadFromJsonAsync<Z190_Bitacora>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Z190_Bitacora>> Buscar(string clave, string orgX)
        {
            var resultado = $"{path}filtro?clave={clave}&orgX={orgX}";

            return await _httpClient.GetFromJsonAsync<IEnumerable<Z190_Bitacora>>(resultado);
        }
    }
}

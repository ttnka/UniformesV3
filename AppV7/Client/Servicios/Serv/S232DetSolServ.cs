using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using System.Net.Http.Json;

namespace AppV7.Client.Servicios.Serv
{
    public class S232DetSolServ : I232DetSolServ
    {
        private readonly HttpClient _httpClient;

        public S232DetSolServ(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        string path = "/api/C232DetSol/";
        public async Task<Z232_DetSol> AddDetalle(Z232_DetSol det)
        {
            var newDet = await _httpClient.PostAsJsonAsync<Z232_DetSol>(path, det);
            if (newDet.IsSuccessStatusCode)
            {
                return await newDet.Content.ReadFromJsonAsync<Z232_DetSol>();
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Z232_DetSol>> Buscar(string clave)
        {
            var resultado = $"{path}filtro?clave={clave}";
            return await _httpClient.GetFromJsonAsync<IEnumerable<Z232_DetSol>>(resultado);
        }

        public async Task<Z232_DetSol> UpDateDetalle(Z232_DetSol det)
        {
            var updateDet = await _httpClient.PutAsJsonAsync<Z232_DetSol>(path, det);
            return updateDet.IsSuccessStatusCode ?
                await updateDet.Content.ReadFromJsonAsync<Z232_DetSol>() : null;
        }
    }
}

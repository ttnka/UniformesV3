using AppV7.Shared;

namespace AppV7.Server.Models.IFace
{
    public interface I804WebCaptura
    {
        Task<IEnumerable<Z804_WebCaptura>> Buscar(string clave);
        Task<Z804_WebCaptura> AddWebCaptura(Z804_WebCaptura wCaptura);
        Task<Z804_WebCaptura> UpdateWebCaptura(Z804_WebCaptura wCaptura);
    }
}

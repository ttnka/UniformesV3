using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using AppV7.Shared.Libreria;
using Microsoft.AspNetCore.Components;

namespace AppV7.Client.Pages.Sistema
{
    public class NewPassBase : ComponentBase
    {
        [Inject]
        public IAddUsuarioServ AddUserIServ { get; set; }
        [Parameter]
        public string Clave { get; set; } = string.Empty;
        public EAddUsuario NewAddUsuario { get; set; } = new();
        public async void ActualizarPass()
        {
           
        }
        [Inject]
        public I190BitacoraServ BitacoraIServ { get; set; }
        public MyFunc MyFunc { get; set; } = new MyFunc();
        public async Task Escribir(string usuarioId, string ordId,
            string desc, bool sistema)
        {
            Z190_Bitacora bita = new Z190_Bitacora();
            bita = MyFunc.WriteBitacora(usuarioId, ordId, desc, sistema);
            await BitacoraIServ.AddBitacora(bita);
        }

    }
}

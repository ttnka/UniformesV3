using Microsoft.AspNetCore.Components;
using AppV7.Shared;
using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared.Libreria;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;

namespace AppV7.Client.Pages.Zuver.WebZuver
{
    public class ContactanosBase : ComponentBase
    {
        public Z840_Contactanos ElContGeneral { get; set; } = new();
        public Z840_Contactanos ElContPub { get; set; } = new();

        [Inject]
        public I800WebSiteServ WebSIServ { get; set; }
        [Inject]
        public I840ContactanosServ ContIServ { get; set; }
        [Parameter]
        public Dictionary<string, string> DatosDic { get; set; } =
            new Dictionary<string, string>();
        
        public bool Habilitar { get; set; } = false;
        //public string[] ArrayDirT = { "Fotografia", "Nombre", "Puesto", "Mail", "Tel", "Cel", "Redes"};
        //public string[] ArrayMapaT = { "Titulo", "Latitud", "Logitud", "Comentario" };
        protected override async Task OnInitializedAsync()
        {
            await ElGeneral();
        }

        public async Task LeerDatos()
        {
            var uno = await ContIServ.Buscar("General");
            if (uno.Any()) ElContGeneral = uno.FirstOrDefault()!;
        }
        public async Task SaveContactanos()
        {
            await ContIServ.UpdateContactanos(ElContGeneral);
        }


        public async Task ElGeneral()
        {
            var elGral = await ContIServ.Buscar("General");
            if (elGral.Any())
            {
                ElContGeneral = elGral.FirstOrDefault()!;
            } else 
            {
                ElContGeneral.General = true;
                
                ElContGeneral = await ContIServ.AddContactanos(ElContGeneral);
            }

        }
       
        [Inject]
        public I190BitacoraServ BitacoraIServ { get; set; }
        public MyFunc MyFunc { get; set; } = new MyFunc();
        public async Task Escribir(string usuarioId, string ordId,
            string desc, bool sistema)
        {
            var bita = MyFunc.WriteBitacora(usuarioId, ordId, desc, sistema);
            await BitacoraIServ.AddBitacora(bita);
        }

        
    }
}

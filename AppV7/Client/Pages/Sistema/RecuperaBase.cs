using AppV7.Client.Servicios.IFaceServ;
using AppV7.Shared;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace AppV7.Client.Pages.Sistema
{
    public class RecuperaBase : ComponentBase 
    {
        [Inject]
        public IAddUsuarioServ AddUserIServ { get; set; }
        public ERecupera RecuperaNew {get;set;} = new ERecupera();
        
        public async Task RecuperaInn()
        {
            await AddUserIServ.Recupera(RecuperaNew);


        }
    }
    
}

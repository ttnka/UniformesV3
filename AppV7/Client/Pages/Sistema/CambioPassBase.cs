using AppV7.Shared;
using Microsoft.AspNetCore.Components;

namespace AppV7.Client.Pages.Sistema
{
    public class CambioPassBase : ComponentBase
    {
        [Parameter]
        public Z110_Usuarios ElUsuario { get; set; } 
        public EAddUsuario EUsuario { get; set; } = new();
        
    }
}

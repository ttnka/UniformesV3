using AppV7.Client.Servicios.IFaceServ;
using Microsoft.AspNetCore.Components;

namespace AppV7.Client.Pages.Sistema
{
    public class ArranqueBase : ComponentBase
    {
        [Inject]
        public IRegistroInicialServ riIServ { get; set; }
        [Inject]
        public NavigationManager NM { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await riIServ.DoRegInicial();
            NM.NavigateTo("/");
        }

    }
}

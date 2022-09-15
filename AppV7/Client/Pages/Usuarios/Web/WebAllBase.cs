using Microsoft.AspNetCore.Components;

namespace AppV7.Client.Pages.Usuarios.Web
{
    public class WebAllBase : ComponentBase 
    {
        public int selectedIndex { get; set; } = 0;
        
        protected async override Task OnInitializedAsync()
        {
            await SIndex();
        }
        public async Task SIndex()
        {
            var valor = DateTime.Now.Second;
            for (int i = 0; i < 15; i++)
            {
                if (valor > 4 )
                { valor -= 5; } else { i = 20; }
            }
            selectedIndex = valor;
        }
    }
}

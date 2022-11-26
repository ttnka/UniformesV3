using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace AppV7.Client.Pages.Uniformes.Canano.Metas
{
    public class MetasGeneralBase : ComponentBase
    {
        [Parameter]
        public string TituloGrafica { get; set; } = string.Empty;
        [Parameter]
        public string SubTituloGrafiaca { get; set; } = string.Empty;
        [Parameter]
        public string TituloSerie { get; set; } = string.Empty;
        [Parameter]
        public bool ShowEtiquetas { get; set; } = true;
        [Parameter]
        public bool ShowAnotacion { get; set; } = false;
        [Parameter]
        public string Anotacion { get; set; } = string.Empty;
        [Parameter]
        public string DatosTitulos { get; set; } = string.Empty;
        [Parameter]
        public string DatosValores { get; set; } = string.Empty;
        [Parameter]
        public string TituloY { get; set; } = string.Empty;
        [Parameter]
        public string MinX { get; set; } = string.Empty;
        public LosDatos[] LaData { get; set; } = default!;
        public int Ultimo { get; set; } = 0;
        protected override void OnInitialized()
        {
            LaData = LeerData(DatosTitulos, DatosValores);
            ColorSchemes =  ElColor();
        }

        public LosDatos[] LeerData(string titulos, string valores) 
        {
            var titulosTemp = titulos.Split(",");
            var valoresTemp = valores.Split(",");
            Ultimo = titulosTemp.Length;
            LosDatos[] Res = new LosDatos[Ultimo];
            for (var i = 0; i < titulosTemp.Length; i ++)
            {
                Res[i] = new LosDatos { Titulo= titulosTemp[i], Valor= int.Parse(valoresTemp[i]) };
            }
            
            return Res;
        }

        public string FormatoY(object value)
        {
            return ((double)value).ToString("N0");
        }
        public class LosDatos
        {
            public string Titulo { get; set; } = null!;
            public int Valor { get; set; }
        }

        public ColorScheme ColorSchemes { get; set; }
        public ColorScheme ElColor()
        {
            Random rnd = new Random();
            int valor = rnd.Next(1, 10);
            
            switch (valor) 
            {
            case int n when ( n > 0 &&  n <= 3):
                  return  ColorScheme.Palette;

            case int n when (n > 3 && n <= 5):
                    return ColorScheme.Palette;

            case int n when (n > 5 && n <= 7):
                    return ColorScheme.Divergent;

            default:
                    return ColorScheme.Monochrome;
            }
           
        }
        


    }
}

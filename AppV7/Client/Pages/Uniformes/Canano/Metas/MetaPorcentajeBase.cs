using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using static AppV7.Client.Pages.Uniformes.Canano.Metas.MetasGeneralBase;

namespace AppV7.Client.Pages.Uniformes.Canano.Metas
{
    public class MetaPorcentajeBase : ComponentBase 
    {
        [Parameter]
        public string TituloGrafica { get; set; } = string.Empty;
        [Parameter]
        public string SubTituloGrafiaca { get; set; } = string.Empty;
        [Parameter]
        public string TituloSerie { get; set; } = string.Empty;
        [Parameter]
        public bool ShowEtiquetas { get; set; } = false;
        [Parameter]
        public bool ShowAnotacion { get; set; } = false;
        [Parameter]
        public string Anotacion { get; set; } = string.Empty;
        
        [Parameter]
        public double Valor { get; set; } = 80;

        public IEnumerable<GaugeTickPosition> tickPositions = 
            Enum.GetValues(typeof(GaugeTickPosition)).Cast<GaugeTickPosition>();

        public GaugeTickPosition tickPosition = GaugeTickPosition.Outside;


    }
}

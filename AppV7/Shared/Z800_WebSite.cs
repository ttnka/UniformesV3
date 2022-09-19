using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class Z800_WebSite
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Nivel { get; set; } = 1;
        public string Catalogo { get; set; } = "10-10-10-10";
        public int Indice { get; set; } = 0;
        public string Titulo { get; set; } = string.Empty;
        public string? Valor { get; set; } = "Mostrar";
        public string? Componente { get; set; } = string.Empty;
        public string? Web { get; set; } = string.Empty;
        public string? Captura { get; set; } = string.Empty;
        public string? TipoValor { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int Estado { get; set; } = 2;
        public bool Status { get; set; } = true;

    }
}

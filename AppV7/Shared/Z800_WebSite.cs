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
        public int Indice { get; set; } = 0;
        public string Titulo { get; set; } = string.Empty;
        public string? Ceja { get; set; } 
        public string? Ayuda { get; set; }
        public string? Componente { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public bool Visible { get; set; } = true;
        public int Estado { get; set; } = 2;
        public bool Status { get; set; } = true;

    }
}

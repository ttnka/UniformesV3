using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class Z804_WebCaptura
    {
        [Key]
        public string CapturaId { get; set; } = Guid.NewGuid().ToString();
        public string Pagina { get; set; } = null!;
        public DateTime FIni { get; set; } = DateTime.Now;
        public DateTime? FOut { get; set; } = DateTime.Now;
        public string? Titulo { get; set; } = string.Empty;
        public string? SubTitulo { get; set; } = string.Empty;
        public string? Nota1 { get; set; } = string.Empty;
        public string? Nota2 { get; set; } = string.Empty;
        public string? Nota3 { get; set; } = string.Empty;
        public string? NotaEspecial { get; set; } = string.Empty;
        public string? Publicidad1 { get; set; } = string.Empty;
        public string? Publicidad2 { get; set; } = string.Empty;
        public string? Publicidad3 { get; set; } = string.Empty;
        public string? PublicidadEspecial { get; set; } = string.Empty;
        public int Estado { get; set; } = 1;
        public bool Status { get; set; } = true;
    }
}

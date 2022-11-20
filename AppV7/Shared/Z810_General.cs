using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class Z810_General
    {
        [Key]
        public string GeneralId { get; set; } = Guid.NewGuid().ToString();
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Pagina { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public string? Subtitulo { get; set; }
        public string Nota { get; set; } = null!;
        public string? Comentario { get; set; }
        public string Org { get; set; } = null!;
        public int Estado { get; set; } = 2;
        public bool Status { get; set; } = true;
    }
}

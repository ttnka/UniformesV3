using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class Z812_Files
    {
        [Key]
        public string FileId { get; set; } = Guid.NewGuid().ToString();
        public DateTime Fecha { get; set; }= DateTime.Now;
        public string Fuente { get; set; } = null!;
        public string FuenteId { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public string Archivo { get; set; } = null!;
        public string? Gpo { get; set; }
        public string Org { get; set; } = null!;
        public int Estado { get; set; } = 2;
        public bool Status { get; set; } = true;
    }
}

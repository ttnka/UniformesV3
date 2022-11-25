using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class Z290_Meta
    {
        [Key]
        public string MetaId { get; set; } = Guid.NewGuid().ToString();
        public string Titulo { get; set; } = null!;        
        public string? Desc { get; set; } 
        public string Tipo { get; set; } = null!;
        public int Cantidad { get; set; }
        public string? Usuario { get; set; }
        public string? Municipio { get; set; }
        public int Estado { get; set; } = 1;
        public bool Status { get; set; } = true;

    }
}

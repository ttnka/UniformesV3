using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class Z220_Producto
    {
        [Key]
        public string ProductoId { get; set; } = Guid.NewGuid().ToString();
        public string Corto { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Desc { get; set; }
        public string Grupo { get; set; } = null!;
        public string Talla { get; set; } = null!;
        public int Estado { get; set; } = 1;
        public bool Status { get; set; } = true;
    }
}

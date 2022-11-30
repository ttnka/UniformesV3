using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class Z210_Almacen
    {
        public Z210_Almacen()
        {
            this.Solicitudes = new HashSet<Z230_Solicitud>();
        }

        [Key]
        public string AlmacenId { get; set; } = Guid.NewGuid().ToString();
        public string Corto { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Desc { get; set; }
        public string Domicilio { get; set; } = null!;
        public string Municipio { get; set; } = null!;
        public int Estado { get; set; } = 1;
        public bool Status { get; set; } = true;
        public virtual ICollection<Z230_Solicitud> Solicitudes { get; set; }
    }
}

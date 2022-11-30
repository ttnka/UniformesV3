using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class Z230_Solicitud
    {
        public Z230_Solicitud()
        {
            this.DetSols = new HashSet<Z232_DetSol>();
        }

        [Key]
        public string SolicitudId { get; set; } = Guid.NewGuid().ToString();
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Folio { get; set; } = null!;
        [ForeignKey("AlmacenId")]
        public string Almacen { get; set; } = null!;
        [ForeignKey("UsuariosId")]
        public string Usuario { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public string? Desc { get; set; } 
        public int Estado { get; set; } = 4;
        public bool Status { get; set; } = true;
        public virtual Z110_Usuarios? Usuarios { get; set; }
        public virtual Z210_Almacen? Almacenes { get; set; }
        
        public virtual ICollection<Z232_DetSol> DetSols { get; set; } 

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class Z232_DetSol
    {
        [Key]
        public string DetId { get; set; } = Guid.NewGuid().ToString();
        public string Folio { get; set; } = null!;
        public string Producto { get; set; } = null!;
        public int Cantidad { get; set; } = 1;   
        public string? Desc { get; set; }
        public int Estado { get; set; } = 1;
        public bool Status { get; set; } = true;
        [ForeignKey("SolicitudId")]
        public string SolicitudId { get; set; }
        public virtual Z230_Solicitud? Solicitudes { get; set; }

    }
}

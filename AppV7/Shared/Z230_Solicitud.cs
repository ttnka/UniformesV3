using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class Z230_Solicitud
    {
        [Key]
        public string SolicitudId { get; set; } = Guid.NewGuid().ToString();
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Folio { get; set; } = null!;
        public string Almacen { get; set; } = null!;
        public string Usuario { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public string? Desc { get; set; } 
        public int Estado { get; set; } = 4;
        public bool Status { get; set; } = true;
    }
}

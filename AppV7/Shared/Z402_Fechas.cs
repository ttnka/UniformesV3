using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class Z402_Fechas
    {
        [Key]
        public string FechaId { get; set; } = Guid.NewGuid().ToString();
        public string CalId { get; set; } = null!;
        public DateTime FecIni { get; set; } = DateTime.Now;
        public DateTime FecFin { get; set; } = DateTime.Now.AddMinutes(15);
        public string? Desc { get; set; }
        public int Estado { get; set; } = 1;
        public bool Status { get; set; } = true;

    }
}

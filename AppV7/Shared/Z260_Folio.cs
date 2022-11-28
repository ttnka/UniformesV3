using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class Z260_Folio
    {
        [Key]
        public string FolioId { get; set; } = Guid.NewGuid().ToString();
        public string? RegId { get; set; } 
        public DateTime FechaEntrega { get; set; } = DateTime.Now;
        public string? Status { get; set; } 
        public string Folio { get; set; } = null!;
        public string? NombreCompleto { get; set; }
        public string? Curp { get; set; }
        public string? TurnoId { get; set; }
        public string? Grado { get; set; } 
        public string? Codigo { get; set; } 
        public string? EscuelaId { get; set; }
        public string? InscStatusId { get; set; }
        public string? GeneroId { get; set; }
        public string? NivelId { get; set; }
        public string? TipoValId { get; set; }
        public string? Localidad { get; set; }
        public string? Municipio { get; set; }

    }
}

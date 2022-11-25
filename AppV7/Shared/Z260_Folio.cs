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
        public string RegId { get; set; } = null!;
        public DateTime FechaEntrega { get; set; } = DateTime.Now;
        public int Status { get; set; } = 0;
        public string Folio { get; set; } = null!;
        public string NombreCompleto { get; set; } = null!;
        public string Curp { get; set; } = null!;
        public string TurnoId { get; set; } = null!;
        public int Grado { get; set; } = 0;
        public string Codigo { get; set; } = null!;
        public string EscuelaId { get; set; } = null!;
        public string InscStatusId { get; set; } = null!;
        public string GeneroId { get; set; } = null!;
        public string NivelId { get; set; } = null!;
        public string TipoValId { get; set; } = null!;
        public string Localidad { get; set; } = null!;
        public string Municipio { get; set; } = null!;

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class Z202_Contacto
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string OrgId { get; set; } = string.Empty;
        public string? UsuarioId { get; set; }
        public string UsuarioName { get; set; } = string.Empty;
        public int tipo { get; set; } = 0;
        public string? Puesto { get; set; }
        public int Estado { get; set; } = 2;
        public bool Status { get; set; } = true;

    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    [Index(nameof(OrgId), IsUnique = false)]
    public class Z110_Usuarios
    {
        [Key]
        public string UsuariosId { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Paterno { get; set; } = null!;
        public string? Materno { get; set; }
        public int Nivel { get; set; } = UserNivel.Participante;
        public string OrgId { get; set; } = null!;
        public string? OldEmail { get; set; }
        public string? Municipio { get; set; }
        public int Estado { get; set; } = 2;
        public bool Status { get; set; } = true;
   
    }
}

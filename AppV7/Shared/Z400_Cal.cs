using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class Z400_Cal
    {
        [Key]
        public string CalId { get; set; } = Guid.NewGuid().ToString();
        public string OrgId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string Grupo { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public string? Desc { get; set; } 
        public bool Privado { get; set; } = false;
        public int Estado { get; set; } = 2;
        public bool Status { get; set; } = true;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class Z205_DatosTipo
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Grupo { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
        public int Estado { get; set; } = 2;
        public bool Status { get; set; } = true;
    }
}

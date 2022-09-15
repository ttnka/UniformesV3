using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class Z800_WebSite
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Padre { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public bool Visible { get; set; } = true;
        public int Estado { get; set; } = 2;
        public bool Status { get; set; } = true;

    }
}

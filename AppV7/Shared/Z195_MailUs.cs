using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class Z195_MailUs
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Para { get; set; } = string.Empty;
        public string Cel { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public int Estado { get; set; } = 2;
        public bool Status { get; set; } = true;

    }
}

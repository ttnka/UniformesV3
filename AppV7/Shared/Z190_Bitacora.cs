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
    public class Z190_Bitacora
    {
        [Key]
        public string BitacoraId { get; set; } = Guid.NewGuid().ToString();
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string UsuariosId { get; set; } = null!;
        public string Desc { get; set; } = null!;
        public bool Sistema { get; set; } = false;
        public string OrgId { get; set; } = null!; 

    }
}

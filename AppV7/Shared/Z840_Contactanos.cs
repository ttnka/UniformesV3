using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class Z840_Contactanos
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool General { get; set; } = false;
        public bool Formato { get; set; } = true;
        public string? ParaMail { get; set; } 
        public bool Mapa { get; set; } = false;
        public string? Titulo { get; set; } 
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        

        public bool Directorio { get; set; } = false;
        public bool Foto { get; set; } = false;
        public bool Nombre { get; set; } = false;
        public bool Puesto { get; set; } = false;
        public bool Mail { get; set; } = false;
        public bool Tel { get; set; } = false;
        public bool Cel { get; set; } = false;
        public bool Redes { get; set; } = false;

        public string? Comentario { get; set; } = string.Empty;
        public string? Domicilio { get; set; } = string.Empty;
        public string? Telefonos { get; set; } = string.Empty;
        public int Estado { get; set; } = 1;
        public bool Status { get; set; } = true;

    }
}

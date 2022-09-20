using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class Z802_WebConfig
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Pagina { get; set; } = null!;
        public string Componente { get; set; } = null!;
        public bool Titulo { get; set; } = true;
        public bool SubTitulo { get; set; } = true;
        public bool Foto { get; set; } = true;
        public bool Archivo { get; set; } = true;
        public bool Carrucel { get; set; }
        public bool Nota1 { get; set; } = true;
        public bool Nota2 { get; set; } = true;
        public bool Nota3 { get; set; } = true;
        public bool NotaEspecial { get; set; } = false;
        public bool Publicidad1 { get; set; } = true;
        public bool Publicidad2 { get; set; } = true;
        public bool Publicidad3 { get; set; } = true;
        public bool PublicidadEspecial { get; set; } = false;
        public int Estado { get; set; } = 2;
        public bool Status { get; set; } = true;

    }
}

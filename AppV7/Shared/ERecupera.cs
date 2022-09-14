using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppV7.Shared
{
    public class ERecupera
    {
        [EmailAddress]
        public string Emial { get; set; } = string.Empty;
        public bool IsRecupera { get; set; } = false;

    }
}

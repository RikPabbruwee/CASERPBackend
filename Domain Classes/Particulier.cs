using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain_Classes
{
    public class Particulier : Cursist
    {
        [Required, StringLength(200)]
        public string Straatnaam { get; set; } = "";
        [Required, StringLength(6)]
        public string Huisnummer { get; set; } = "";
    }
}

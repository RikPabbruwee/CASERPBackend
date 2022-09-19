using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain_Classes
{
    public class Cursist
    {
        [Required]
        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Voornaam { get; set; } = "";
        [Required, StringLength(200)]
        public string Achternaam { get; set; } = "";
    }
}

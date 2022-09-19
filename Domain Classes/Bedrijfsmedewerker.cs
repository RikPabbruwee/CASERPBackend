using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain_Classes
{
    public class Bedrijfsmedewerker : Cursist
    {
        [Required, StringLength(200)]
        public string Bedrijfsnaam = "";
        [Required, StringLength(200)]
        public string Afdeling = "";
        [Required, StringLength(30)]
        public string Offertenummer = "";

    }
}

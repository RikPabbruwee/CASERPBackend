using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain_Classes
{
    public class CursusInstantie
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Startdatum { get; set; }
        [Required]
        public virtual Cursus Cursus { get; set; }
        public virtual List<Cursist> Cursisten { get; set; }
    }
}

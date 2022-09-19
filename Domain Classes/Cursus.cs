using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Classes
{
    public class Cursus
    {
        public int Id { get; set; }
        //Minimal 1 day, max 5 days 
        [Required, Range(1,5)]
        public int Duur { get; set; }
        [Required, StringLength (300)]
        public string Titel { get; set; }  = "";
    }
}

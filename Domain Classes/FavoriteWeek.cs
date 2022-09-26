using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Classes
{
    public  class FavoriteWeek
    {
        public int Id { get; set; }
        [Required]
        public int Week { get; set; }
        [Required]
        public int Year { get; set; }
    }
}

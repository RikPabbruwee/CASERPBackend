using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class CursusInstantieDTO
    {
        public int Id { get; set; } = 0;
        [Required, StringLength(300)]
        public string Titel { get; set; } = "";
        public int CursusId { get; set; } = 0;
        [Required, StringLength(10)]
        public string CursusCode { get; set; } = "";
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [Required, Range(1, 5)]
        public int Duration { get; set; } = 0;
        public int Cursusisten { get; set; } = 0;
    }
}

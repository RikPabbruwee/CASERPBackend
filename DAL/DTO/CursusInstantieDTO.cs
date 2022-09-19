using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class CursusInstantieDTO
    {
        public int Id { get; set; } = 0;
        public string Titel { get; set; } = "";
        public int CursusId { get; set; } = 0;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public int Duration { get; set; } = 0;
        public int Cursusisten { get; set; } = 0;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DuplicationDTO
    {
        public int AmountOfDuplictes { get; set; }
        public List<string> DuplicatesCodes { get; set; } = new List<string>();
    }
}

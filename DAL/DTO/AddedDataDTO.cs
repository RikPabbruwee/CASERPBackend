using Domain_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class AddedDataDTO
    {
        public List<CursusInstantieDTO> DuplicateCursusInstanties { get; set; } = new List<CursusInstantieDTO>();
        public List<CursusInstantieDTO> NewCursusInstanties { get; set;} = new List<CursusInstantieDTO>();
        public List<CursusInstantieDTO> OutOfRangeCursusInstanties {  get; set;} = new List<CursusInstantieDTO>();
        public List<Cursus> NewCursus { get; set;} = new List<Cursus>();
    }
}

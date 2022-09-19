using Domain_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface ICursusInstantieRepository
    {
        public Task<List<CursusInstantie>> GetCursusInstanties();
        public Task<CursusInstantie?> GetCursusInstantieById(int id);
        public Task InsertCursusInstantie(CursusInstantie cursusInstantie);
        public Task Save();
    }
}

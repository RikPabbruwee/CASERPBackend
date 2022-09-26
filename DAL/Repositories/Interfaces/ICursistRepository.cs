using Domain_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface ICursistRepository
    {
        public Task InsertCursistAsync(CursusInstantie cursusInstantie, Cursist cursist);
        public Task<List<Cursist>> GetAllCursistsAsync();
        public Task<List<Cursist>> GetCursistsByCursusInstantieAsync(CursusInstantie cursusInstantie);
    }
}

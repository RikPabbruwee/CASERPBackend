using Domain_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface ICursusRepository
    {
        public Task<IEnumerable<Cursus>> GetCursussen();
        public Task<Cursus?> GetCursusById(int id);
        public Task<Cursus?> GetCursusByCode(string code);
        public Task InsertCursus(Cursus cursus);
        public Task Save();

    }
}

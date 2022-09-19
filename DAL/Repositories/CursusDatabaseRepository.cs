using DAL.DataAcess;
using DAL.Repositories.Interfaces;
using Domain_Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CursusDatabaseRepository : ICursusRepository
    {
        private CursusContext _context;
        public CursusDatabaseRepository(CursusContext context)
        {
            _context = context;
        }

        public async Task<Cursus?> GetCursusById(int id)
        {
             return await _context.Cursussen
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Cursus>> GetCursussen()
        {
            return await _context.Cursussen.ToListAsync();
        }

        public async Task InsertCursus(Cursus cursus)
        {
            await _context.Cursussen.AddAsync(cursus);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}

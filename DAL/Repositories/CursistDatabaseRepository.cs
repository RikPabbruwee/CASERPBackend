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
    public class CursistDatabaseRepository : ICursistRepository
    {
        CursusContext _context;
        public CursistDatabaseRepository(CursusContext context) {
            _context = context;
        }
        public async Task<List<Cursist>> GetAllCursistsAsync()
        {
            return await _context.Cursisten.ToListAsync();
        }

        public async Task<List<Cursist>> GetCursistsByCursusInstantieAsync(CursusInstantie cursusInstantie)
        {
            return await _context.Cursisten.Where(x => cursusInstantie.Cursisten.Contains(x)).ToListAsync();
        }

        public async Task InsertCursistAsync(CursusInstantie cursusInstantie ,Cursist cursist)
        {
            _context.Cursisten.Add(cursist);
            cursusInstantie.Cursisten.Add(cursist);
            _context.CursusInstanties.Update(cursusInstantie);
            await _context.SaveChangesAsync();
        }
    }
}

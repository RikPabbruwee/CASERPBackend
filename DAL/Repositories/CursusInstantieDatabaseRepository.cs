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
    public class CursusInstantieDatabaseRepository : ICursusInstantieRepository
    {
        private CursusContext _context;
        public CursusInstantieDatabaseRepository(CursusContext context)
        {
            _context = context;
        }

        public async Task<CursusInstantie?> GetCursusInstantieById(int id)
        {
            return await _context.CursusInstanties.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CursusInstantie>> GetCursusInstanties()
        {
            return await _context.CursusInstanties.Include(x => x.Cursus).ToListAsync();
        }
        public async Task<List<CursusInstantie>> GetCursusInstantiesByWeek(DateTime week)
        {
            //Get first and last day of the given day;
            var culture = Thread.CurrentThread.CurrentCulture;
            var diff = week.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            diff = diff < 0 ? diff+=7 : diff;
            DateTime firstDay = week.AddDays(-diff).Date;
            DateTime lastDay = firstDay.AddDays(4);
            return await _context.CursusInstanties
                .Include(x => x.Cursus)
                .Where(x=> x.Startdatum > firstDay && x.Startdatum < lastDay)
                .ToListAsync();
        }
        public async Task InsertCursusInstantie(CursusInstantie cursusInstantie)
        {
            await _context.CursusInstanties.AddAsync(cursusInstantie);
            await Save();
        }
        public async Task<CursusInstantie?> FindPossibleDuplicat(CursusInstantie cursusInstantie)
        {
            return await _context.CursusInstanties.FirstOrDefaultAsync(x =>
                x.Startdatum == cursusInstantie.Startdatum &&
                x.Cursus.Duur == cursusInstantie.Cursus.Duur &&
                x.Cursus.Code == cursusInstantie.Cursus.Code
            );
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}

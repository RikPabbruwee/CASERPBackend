using DAL.DataAcess;
using DAL.Repositories.Interfaces;
using Domain_Classes;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class FavoriteWeekDatabaseRepository : IFavoriteWeekRepository
    {
        private CursusContext _context;
        public FavoriteWeekDatabaseRepository(CursusContext context) {
            _context = context;
        }
        public async Task<List<FavoriteWeek>> GetWeeks()
        {
            return await _context.FavoriteWeeks.ToListAsync();
        }

        public async Task InsertWeek(int week, int year)
        {
            FavoriteWeek newWeek = new FavoriteWeek();
            newWeek.Week = week;
            newWeek.Year = year;
            _context.FavoriteWeeks.Add(newWeek);
            await _context.SaveChangesAsync();
        }
    }
}

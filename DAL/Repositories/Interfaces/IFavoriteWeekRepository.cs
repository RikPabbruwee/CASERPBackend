using Domain_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IFavoriteWeekRepository
    {
        Task InsertWeek(int week, int year);
        Task<List<FavoriteWeek>> GetWeeks();
    }
}

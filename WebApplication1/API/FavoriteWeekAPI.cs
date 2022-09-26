
using DAL.DTO;
using DAL.Repositories.Interfaces;
using Domain_Classes;
using Microsoft.AspNetCore.Mvc;

namespace backend.API
{
    [ApiController]
    [Route("api/favoriteWeek")]
    public class FavoriteWeekAPI : Controller
    {
        private IFavoriteWeekRepository _favoriteWeekRepository;
        public FavoriteWeekAPI(IFavoriteWeekRepository favoriteWeekRepository)
        {
            _favoriteWeekRepository = favoriteWeekRepository;
        }
        [HttpGet]
        public async Task<List<FavoriteWeek>> GetFavoriteWeeksAsync()
        {
            return await _favoriteWeekRepository.GetWeeks();
        }
        [HttpPost]
        public async Task InsertWeek(FavoriteWeekDTO newFavorite) { 
            await _favoriteWeekRepository.InsertWeek(newFavorite.Week, newFavorite.Year);
        }

    }
}

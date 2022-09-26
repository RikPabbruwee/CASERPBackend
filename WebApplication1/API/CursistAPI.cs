using DAL.DTO;
using DAL.Repositories.Interfaces;
using Domain_Classes;
using Microsoft.AspNetCore.Mvc;

namespace backend.API
{
    [ApiController]
    [Route("api/cursist")]
    public class CursistAPI : Controller
    {
        private ICursistRepository _cursistRepository;
        private ICursusInstantieRepository _cursusInstantieRepository;
        public CursistAPI(ICursistRepository cursistRepository, ICursusInstantieRepository cursusInstantieRepository)
        {
            _cursistRepository = cursistRepository;
            _cursusInstantieRepository = cursusInstantieRepository;
        }
        [HttpGet]
        public async Task<List<Cursist>> GetAllCursistenOfInstantie(CursusInstantieDTO inst)
        {            
            return await _cursistRepository.GetCursistsByCursusInstantieAsync(await _cursusInstantieRepository.GetCursusInstantieById(inst.Id));
        }
        [HttpPost]
        public async Task AddAsync()
        {
            
        }        
    }
}

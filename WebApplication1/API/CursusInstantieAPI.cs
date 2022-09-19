using DAL.DTO;
using DAL.Repositories.Interfaces;
using Domain_Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.API
{
    [ApiController]
    [Route("api/cursusinstantie")]
    public class CursusInstantieAPI : Controller
    {
        private ICursusInstantieRepository _cursusInstantieRepository;
        private ICursusRepository _cursusRepository;
        public CursusInstantieAPI(ICursusRepository cursusRepository, ICursusInstantieRepository cursusInstantieRepository)
        {
            _cursusRepository = cursusRepository;
            _cursusInstantieRepository = cursusInstantieRepository;
        }
        [HttpGet]
        public async Task<List<CursusInstantieDTO>> GetAll()
        {
            List<CursusInstantieDTO> list = new List<CursusInstantieDTO>();
            IEnumerable<CursusInstantie> instanties = await _cursusInstantieRepository.GetCursusInstanties();
            foreach(CursusInstantie inst in instanties)
            {
                //Put everything into the DTO
                CursusInstantieDTO dto = new CursusInstantieDTO();
                dto.Id = inst.Id;
                dto.StartDate = inst.Startdatum;
                
                dto.Cursusisten = (inst.Cursisten != null) ? inst.Cursisten.Count : 0;

                //Forgot I put this connection in, hope it works on runtime 🤔
                dto.Duration = inst.Cursus.Duur;
                dto.Titel = inst.Cursus.Titel;
                dto.CursusId = inst.Cursus.Id;
                list.Add(dto);                
            }
            return list;
        }
        [HttpPost]
        public async Task Add(CursusInstantieDTO dto)
        {
            if (dto == null)
            {
                return;
            }
            CursusInstantie inst = new CursusInstantie();
            //cursus.Id = dto.Id;
            inst.Startdatum = dto.StartDate;
            //Check if cursus exist or not 
            //Should be moved to its own provider 
            Cursus? cursus = await _cursusRepository.GetCursusById(dto.CursusId);
            if(cursus == null)
            {
                //Add new cursus 
                cursus = new Cursus();
                cursus.Titel = dto.Titel;
                cursus.Duur = dto.Duration;
                await _cursusRepository.InsertCursus(cursus);
            }
            inst.Cursus = cursus;
            await _cursusInstantieRepository.InsertCursusInstantie(inst);
        }
    }
}

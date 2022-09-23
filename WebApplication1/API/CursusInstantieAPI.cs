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
        
        public async Task<List<CursusInstantieDTO>> GetAll()
        {
            return await GetAll(DateTime.Now);
        }
        [HttpGet]
        public async Task<List<CursusInstantieDTO>> GetAll(DateTime? week)
        {
            List<CursusInstantieDTO> list = new List<CursusInstantieDTO>();
            List<CursusInstantie> instanties = await _cursusInstantieRepository.GetCursusInstantiesByWeek(week ?? DateTime.Now);
            instanties.Sort((x, y) => y.Startdatum.CompareTo(x.Startdatum));
            foreach (CursusInstantie inst in instanties)
            {
                //Put everything into the DTO
                CursusInstantieDTO dto = CreateCursusInstantieDTOFromCursusInstantie(inst);
                list.Add(dto);
            }
            return list;
        }
        [HttpPost]
        public async Task<ActionResult<AddedDataDTO>> Add(List<CursusInstantieDTO> list, DateTime? startDate, DateTime? endDate)
        {
            AddedDataDTO Feedback = new AddedDataDTO();
            foreach (CursusInstantieDTO dto in list)
            {
                if (dto == null)
                {
                    //return duplicatList;
                    return Problem("Data not correct!");
                }

                CursusInstantie newCursusInst = new CursusInstantie();
                //cursus.Id = dto.Id;
                newCursusInst.Startdatum = dto.StartDate;
                //Check if cursus exist or not 
                //Should be moved to its own provider 
                Cursus? cursus = await _cursusRepository.GetCursusByCode(dto.CursusCode);
                if (cursus == null)
                {
                    //Create new cursus 
                    cursus = new Cursus();
                    cursus.Titel = dto.Titel;
                    cursus.Duur = dto.Duration;
                    cursus.Code = dto.CursusCode;
                    await _cursusRepository.InsertCursus(cursus);
                    Feedback.NewCursus.Add(cursus);
                }

                newCursusInst.Cursus = cursus;
                CursusInstantie? possibleDuplicat = await _cursusInstantieRepository.FindPossibleDuplicat(newCursusInst);
                //Date check

                if (startDate != null && endDate != null) { 
                    if (dto.StartDate > startDate || dto.StartDate < endDate)
                    {
                        Feedback.OutOfRangeCursusInstanties.Add(CreateCursusInstantieDTOFromCursusInstantie(newCursusInst));
                        continue;
                    }
                }
                if (possibleDuplicat != null)
                {
                    Feedback.DuplicateCursusInstanties.Add(CreateCursusInstantieDTOFromCursusInstantie(possibleDuplicat));
                }
                else
                {
                    await _cursusInstantieRepository.InsertCursusInstantie(newCursusInst);
                    Feedback.NewCursusInstanties.Add(CreateCursusInstantieDTOFromCursusInstantie(newCursusInst));
                }
                    
            }
            return Ok(Feedback);
        }
        private CursusInstantieDTO CreateCursusInstantieDTOFromCursusInstantie(CursusInstantie input)
        {
            CursusInstantieDTO dto = new CursusInstantieDTO();
            dto.Id = input.Id;
            dto.StartDate = input.Startdatum;

            dto.Cursusisten = (input.Cursisten != null) ? input.Cursisten.Count : 0;

            //Forgot I put this connection in, hope it works on runtime 🤔
            //inst.Cursus = _cursusRepository.GetCursusById(inst.cur)
            dto.Duration = input.Cursus.Duur;
            dto.Titel = input.Cursus.Titel;
            dto.CursusId = input.Cursus.Id;
            dto.CursusCode = input.Cursus.Code;
            return dto;
        }
    }
}

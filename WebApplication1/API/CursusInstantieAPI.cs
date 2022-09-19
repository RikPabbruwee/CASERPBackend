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
            List<CursusInstantie> instanties = await _cursusInstantieRepository.GetCursusInstanties();
            instanties.Sort((x, y) => y.Startdatum.CompareTo(x.Startdatum));
            foreach(CursusInstantie inst in instanties)
            {
                //Put everything into the DTO
                CursusInstantieDTO dto = new CursusInstantieDTO();
                dto.Id = inst.Id;
                dto.StartDate = inst.Startdatum;
               
                dto.Cursusisten = (inst.Cursisten != null) ? inst.Cursisten.Count : 0;

                //Forgot I put this connection in, hope it works on runtime 🤔
                //inst.Cursus = _cursusRepository.GetCursusById(inst.cur)
                dto.Duration = inst.Cursus.Duur;
                dto.Titel = inst.Cursus.Titel;
                dto.CursusId = inst.Cursus.Id;
                dto.CursusCode = inst.Cursus.Code;
                list.Add(dto);                
            }
            return list;
        }
        [HttpPost]
        public async Task<ActionResult<DuplicationDTO>> Add(List<CursusInstantieDTO> list)
        {
            DuplicationDTO duplicatList = new DuplicationDTO();
            foreach(CursusInstantieDTO dto in list)
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
                }
                else
                {
                    duplicatList.DuplicatesCodes.Add(dto.CursusCode);
                    duplicatList.AmountOfDuplictes++;
                }
                newCursusInst.Cursus = cursus;
                await _cursusInstantieRepository.InsertCursusInstantie(newCursusInst);
            }
            return Ok(duplicatList);
        }
    }
}

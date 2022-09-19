﻿using DAL.Repositories.Interfaces;
using Domain_Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.API
{
    
    [ApiController]
    [Route("api/cursus")]

    public class CursusAPI : Controller
    {
        private ICursusRepository _cursusRepository;
        public CursusAPI(ICursusRepository cursusRepository)
        {
            _cursusRepository = cursusRepository;

            [HttpGet]
            async Task <IEnumerable<Cursus>> Get()
            {
                return await _cursusRepository.GetCursussen();
            }
        }
    }
}
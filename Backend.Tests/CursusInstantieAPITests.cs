using backend.API;
using DAL.DTO;
using DAL.Repositories.Interfaces;
using Domain_Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Tests
{
    [TestClass]
    public class CursusInstantieAPITests
    {
        private readonly List<CursusInstantieDTO> _instantiesMockData = new List<CursusInstantieDTO>();
        private Mock<ICursusInstantieRepository> _mockCursusInstantieRepository;
        private Mock<ICursusRepository> _mockCursusRepository;
        private CursusInstantieAPI _sut;
        [TestInitialize]
        public void Setup()
        {
            _mockCursusInstantieRepository = new Mock<ICursusInstantieRepository>(MockBehavior.Strict);
            _mockCursusInstantieRepository
                    .Setup(x => x.GetCursusInstanties())
                    .ReturnsAsync(new List<CursusInstantie>());
            _mockCursusInstantieRepository
                    .Setup(x => x.GetCursusInstantiesByWeek(It.IsAny<DateTime>()))
                    .ReturnsAsync(new List<CursusInstantie>());
            _mockCursusInstantieRepository
                    .Setup(x => x.GetCursusInstantieById(It.IsAny<int>()))
                    .ReturnsAsync(new CursusInstantie());
            _mockCursusInstantieRepository
                    .Setup(x => x.InsertCursusInstantie(It.IsAny<CursusInstantie>()));
            _mockCursusInstantieRepository
                    .Setup(x => x.FindPossibleDuplicat(It.IsAny<CursusInstantie>()))
                    .ReturnsAsync(new CursusInstantie());
            _mockCursusRepository = new Mock<ICursusRepository>(MockBehavior.Strict);
            _mockCursusRepository
                    .Setup(x => x.InsertCursus(It.IsAny<Cursus>()))
                    .ReturnsAsync(new Cursus());
            _mockCursusRepository
                    .Setup(x => x.GetCursusByCode(It.Is<String>(x => x == "")))
                    .ReturnsAsync(() => null);
            _mockCursusRepository
                    .Setup(x => x.GetCursusByCode(It.IsAny<String>()))
                    .ReturnsAsync(new Cursus());
            _sut = new CursusInstantieAPI(_mockCursusRepository.Object, _mockCursusInstantieRepository.Object);
            
            
        }
        [TestMethod]
        public async Task GetAllAsync_WithNoDate()
        {
            // Arrange
            List<CursusInstantieDTO>? returnData = null;
            // Act
            returnData = await _sut.GetAllAsync(null);
            // Assert
            _mockCursusInstantieRepository.Verify(x => x.GetCursusInstantiesByWeek(It.IsAny<DateTime>()), Times.Once());            
            Assert.IsNotNull(returnData);
        }

        [TestMethod]
        public async Task GetAllAsync_WithDate()
        {
            // Arrange
            List<CursusInstantieDTO>? returnData = null;
            // Act
            returnData = await _sut.GetAllAsync(DateTime.Now);
            // Assert
            _mockCursusInstantieRepository.Verify(x => x.GetCursusInstantiesByWeek(It.IsAny<DateTime>()), Times.Once());
            Assert.IsNotNull(returnData);
        }
        [TestMethod]
        public async Task AddAsync_WithNoDatesWithCursusCode()
        {
            // Arrange
            List<CursusInstantieDTO> toInsert = new List<CursusInstantieDTO>();
            CursusInstantieDTO toInsertDTO = new CursusInstantieDTO();
            toInsertDTO.Duration = 4;
            toInsertDTO.CursusCode = "TEST";
            toInsert.Add(toInsertDTO);
            // Act
            await _sut.AddAsync(toInsert, null, null);
            // Assert
            _mockCursusRepository.Verify(x => x.GetCursusByCode(It.IsAny<string>()), Times.AtLeastOnce());
            _mockCursusInstantieRepository.Verify(x => x.FindPossibleDuplicat(It.IsAny<CursusInstantie>()), Times.AtLeastOnce());
        }
        [TestMethod]
        public async Task AddAsync_WithNoDatesWithoutCursusCode()
        {
            // Arrange
            List<CursusInstantieDTO> toInsert = new List<CursusInstantieDTO>();
            CursusInstantieDTO toInsertDTO = new CursusInstantieDTO();
            toInsertDTO.Duration = 4;
            toInsertDTO.CursusCode = "";
            toInsert.Add(toInsertDTO);
            // Act
            await _sut.AddAsync(toInsert, null, null);
            // Assert
            _mockCursusRepository.Verify(x => x.GetCursusByCode(It.IsAny<string>()), Times.AtLeastOnce());
            _mockCursusInstantieRepository.Verify(x => x.FindPossibleDuplicat(It.IsAny<CursusInstantie>()), Times.AtLeastOnce());
        }
    }
}
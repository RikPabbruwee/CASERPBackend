using backend.API;
using DAL.Repositories.Interfaces;
using Domain_Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Tests
{
    [TestClass]
    public class CursistAPITests
    {
        private CursistAPI _sut;
        private Mock<ICursistRepository> _mockCursistRepository;
        private Mock<ICursusInstantieRepository> _mockInstantieRepository;
        [TestInitialize]
        public void Setup()
        {
            _mockCursistRepository = new Mock<ICursistRepository>(MockBehavior.Strict);
            _mockCursistRepository.Setup(x => x.InsertCursistAsync(It.IsAny<CursusInstantie>(), It.IsAny<Cursist>()))
                .Returns(Task.CompletedTask);
            _mockCursistRepository.Setup(x => x.GetCursistsByCursusInstantieAsync(It.IsAny<CursusInstantie>()))
                .ReturnsAsync(new List<Cursist>());

            _mockInstantieRepository = new Mock<ICursusInstantieRepository>(MockBehavior.Strict);
            _mockInstantieRepository.Setup(x => x.GetCursusInstantieById(It.IsAny<int>()))
                .ReturnsAsync(new CursusInstantie());
            _sut = new CursistAPI(_mockCursistRepository.Object, _mockInstantieRepository.Object);
        }
        //[TestMethod]
        //public Task GetAllCursistenOfInstantie()
        //{
        //    // Arrange

        //    // Act

        //    // Assert 
        //}
    }
}

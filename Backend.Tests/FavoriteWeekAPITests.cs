using backend.API;
using DAL.DTO;
using DAL.Repositories.Interfaces;
using Domain_Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Tests
{
    [TestClass]
    public class FavoriteWeekAPITests
    {
        private FavoriteWeekAPI _sut;
        private Mock<IFavoriteWeekRepository> _mockFavoriteWeekRepository;
        [TestInitialize]
        public void Setup()
        {
            _mockFavoriteWeekRepository = new Mock<IFavoriteWeekRepository>(MockBehavior.Strict);
            _mockFavoriteWeekRepository.Setup(x =>
                x.InsertWeek(
                    It.IsInRange<int>(1, 52, Moq.Range.Inclusive),
                    It.IsAny<int>()))
                .Returns(Task.CompletedTask);//I don't know if this is the correct way
            _mockFavoriteWeekRepository.Setup(x =>
                x.GetWeeks()).ReturnsAsync(new List<FavoriteWeek>());
            _sut = new FavoriteWeekAPI(_mockFavoriteWeekRepository.Object);
        }
        [TestMethod]
        public async Task GetWeeks()
        {
            // Arrange
            List<FavoriteWeek>? returnData = null;
            // Act
            returnData = await _sut.GetFavoriteWeeksAsync();
            // Assert
            _mockFavoriteWeekRepository.Verify(x => x.GetWeeks(), Times.Once());
            Assert.IsNotNull(returnData);
        }
        [TestMethod]
        public async Task InsertWeeks()
        {
            // Arrange
            FavoriteWeekDTO toInsert = new FavoriteWeekDTO();
            toInsert.Week = 12;
            toInsert.Year = 2012;
            // Act
            await _sut.InsertWeek(toInsert);
            // Assert
            _mockFavoriteWeekRepository.Verify(
                x => x.InsertWeek(It.IsInRange<int>(1, 52, Moq.Range.Inclusive), It.IsAny<int>()), 
                Times.Once());
        }
    }
}

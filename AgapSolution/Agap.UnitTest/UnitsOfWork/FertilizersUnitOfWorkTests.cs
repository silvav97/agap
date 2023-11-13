using Agap.Backemd.Repositories;
using Agap.Backemd.UnitsOfWork;
using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;
using Moq;

namespace Agap.UnitTest.UnitsOfWork
{
    [TestClass]
    public class FertilizersUnitOfWorkTests
    {
        private Mock<IGenericRepository<Fertilizer>> _mockGenericRepository = null!;
        private Mock<IFertilizersRepository> _mockCategoriesRepository = null!;
        private FertilizersUnitOfWork _unitOfWork = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockGenericRepository = new Mock<IGenericRepository<Fertilizer>>();
            _mockCategoriesRepository = new Mock<IFertilizersRepository>();
            _unitOfWork = new FertilizersUnitOfWork(_mockGenericRepository.Object, _mockCategoriesRepository.Object);
        }

        [TestMethod]
        public async Task GetAsync_CallsRepositoryAndReturnsResult()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var expectedResponse = new Response<IEnumerable<Fertilizer>> { Result = new List<Fertilizer>() };
            _mockCategoriesRepository.Setup(x => x.GetAsync(pagination)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _unitOfWork.GetAsync(pagination);

            // Assert
            Assert.AreEqual(expectedResponse, result);
            _mockCategoriesRepository.Verify(x => x.GetAsync(pagination), Times.Once);
        }

        [TestMethod]
        public async Task GetTotalPagesAsync_CallsRepositoryAndReturnsResult()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var expectedResponse = new Response<int> { Result = 5 };
            _mockCategoriesRepository.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _unitOfWork.GetTotalPagesAsync(pagination);

            // Assert
            Assert.AreEqual(expectedResponse, result);
            _mockCategoriesRepository.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once);
        }
    }
}
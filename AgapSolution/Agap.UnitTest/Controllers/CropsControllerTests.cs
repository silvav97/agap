
using Agap.Backemd.Controllers;
using Agap.Backemd.UnitsOfWork;
using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Agap.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Agap.UnitTest.Controllers
{
    [TestClass]
    public class CropsControllerTests
    {
        private Mock<IGenericUnitOfWork<Crop>> _mockGenericUnitOfWork;
        private Mock<ICropsUnitOfWork> _mockCropsUnitOfWork;
        private CropsController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _mockGenericUnitOfWork = new Mock<IGenericUnitOfWork<Crop>>();
            _mockCropsUnitOfWork = new Mock<ICropsUnitOfWork>();
            _controller = new CropsController(_mockGenericUnitOfWork.Object, _mockCropsUnitOfWork.Object);
        }

        [TestMethod]
        public async Task GetAsync_Pagination_ShouldReturnOk()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var crops = new List<Crop> { /* Populate with test data */ };
            var response = new Response<IEnumerable<Crop>> { WasSuccess = true, Result = crops };
            _mockCropsUnitOfWork.Setup(x => x.GetAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(crops, okResult?.Value);
            _mockCropsUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_Pagination_ShouldReturnBadRequest()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new Response<IEnumerable<Crop>> { WasSuccess = false };
            _mockCropsUnitOfWork.Setup(x => x.GetAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockCropsUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnOk()
        {
            // Arrange
            var crops = new List<Crop> { /* Populate with test data */ };
            var response = new Response<IEnumerable<Crop>> { WasSuccess = true, Result = crops };
            _mockCropsUnitOfWork.Setup(x => x.GetAllAsync()).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(crops, okResult?.Value);
            _mockCropsUnitOfWork.Verify(x => x.GetAllAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ShouldReturnOk()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var totalPages = 5;
            var response = new Response<int> { WasSuccess = true, Result = totalPages };
            _mockCropsUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetPagesAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(totalPages, okResult?.Value);
            _mockCropsUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ShouldReturnBadRequest()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new Response<int> { WasSuccess = false };
            _mockCropsUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetPagesAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockCropsUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task CloseCropAsync_ShouldReturnOk()
        {
            // Arrange
            var cropId = 1;
            var response = new Response<bool> { WasSuccess = true, Result = true };
            _mockCropsUnitOfWork.Setup(x => x.CloseCropAsync(cropId)).ReturnsAsync(response);

            // Act
            var result = await _controller.CloseCropAsync(cropId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(true, okResult?.Value);
            _mockCropsUnitOfWork.Verify(x => x.CloseCropAsync(cropId), Times.Once());
        }

        [TestMethod]
        public async Task CloseCropAsync_ShouldReturnBadRequest()
        {
            // Arrange
            var cropId = 1;
            var response = new Response<bool> { WasSuccess = false, Message = "Error closing crop" };
            _mockCropsUnitOfWork.Setup(x => x.CloseCropAsync(cropId)).ReturnsAsync(response);

            // Act
            var result = await _controller.CloseCropAsync(cropId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("Error closing crop", badRequestResult?.Value);
            _mockCropsUnitOfWork.Verify(x => x.CloseCropAsync(cropId), Times.Once());
        }
    }
}

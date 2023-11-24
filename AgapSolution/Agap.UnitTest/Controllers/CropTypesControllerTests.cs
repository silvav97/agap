using Agap.Backemd.Controllers;
using Agap.Backemd.UnitsOfWork;
using Agap.Shared.DTOs;
using Agap.Shared.Entities.Agap.Shared.Entities;
using Agap.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Agap.UnitTest.Controllers
{
    [TestClass]
    public class CropTypesControllerTests
    {
        private Mock<IGenericUnitOfWork<CropType>> _mockGenericUnitOfWork;
        private Mock<ICropTypesUnitOfWork> _mockCropTypesUnitOfWork;
        private CropTypesController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _mockGenericUnitOfWork = new Mock<IGenericUnitOfWork<CropType>>();
            _mockCropTypesUnitOfWork = new Mock<ICropTypesUnitOfWork>();
            _controller = new CropTypesController(_mockGenericUnitOfWork.Object, _mockCropTypesUnitOfWork.Object);
        }

        [TestMethod]
        public async Task GetAsync_Pagination_ShouldReturnOk()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var cropTypes = new List<CropType>
            {
                new CropType { Id = 1, Name = "CropType1", Weather = "Weather1" },
                new CropType { Id = 2, Name = "CropType2", Weather = "Weather2" }
            };
            var response = new Response<IEnumerable<CropType>> { WasSuccess = true, Result = cropTypes };
            _mockCropTypesUnitOfWork.Setup(x => x.GetAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(cropTypes, okResult?.Value);
            _mockCropTypesUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_Pagination_ShouldReturnBadRequest()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new Response<IEnumerable<CropType>> { WasSuccess = false };
            _mockCropTypesUnitOfWork.Setup(x => x.GetAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockCropTypesUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ShouldReturnOk()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var totalPages = 5;
            var response = new Response<int> { WasSuccess = true, Result = totalPages };
            _mockCropTypesUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetPagesAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(totalPages, okResult?.Value);
            _mockCropTypesUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ShouldReturnBadRequest()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new Response<int> { WasSuccess = false };
            _mockCropTypesUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetPagesAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockCropTypesUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once());
        }
    }
}

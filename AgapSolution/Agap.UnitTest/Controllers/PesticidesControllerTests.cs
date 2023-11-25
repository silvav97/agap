

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
    public class PesticidesControllerTests
    {
        private Mock<IGenericUnitOfWork<Pesticide>> _mockGenericUnitOfWork;
        private Mock<IPesticidesUnitOfWork> _mockPesticidesUnitOfWork;
        private PesticidesController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _mockGenericUnitOfWork = new Mock<IGenericUnitOfWork<Pesticide>>();
            _mockPesticidesUnitOfWork = new Mock<IPesticidesUnitOfWork>();
            _controller = new PesticidesController(_mockGenericUnitOfWork.Object, _mockPesticidesUnitOfWork.Object);
        }

        [TestMethod]
        public async Task GetAsync_Pagination_ShouldReturnOk()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var pesticides = new List<Pesticide>
            {
                new Pesticide { Id = 1, Name = "Pesticide1", Brand = "Brand1", PricePerGram = 10.0f },
                new Pesticide { Id = 2, Name = "Pesticide2", Brand = "Brand2", PricePerGram = 15.0f }
            };
            var response = new Response<IEnumerable<Pesticide>> { WasSuccess = true, Result = pesticides };
            _mockPesticidesUnitOfWork.Setup(x => x.GetAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(pesticides, okResult?.Value);
            _mockPesticidesUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_Pagination_ShouldReturnBadRequest()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new Response<IEnumerable<Pesticide>> { WasSuccess = false };
            _mockPesticidesUnitOfWork.Setup(x => x.GetAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockPesticidesUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ShouldReturnOk()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var totalPages = 5;
            var response = new Response<int> { WasSuccess = true, Result = totalPages };
            _mockPesticidesUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetPagesAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(totalPages, okResult?.Value);
            _mockPesticidesUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ShouldReturnBadRequest()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new Response<int> { WasSuccess = false };
            _mockPesticidesUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetPagesAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockPesticidesUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once());
        }
    }
}

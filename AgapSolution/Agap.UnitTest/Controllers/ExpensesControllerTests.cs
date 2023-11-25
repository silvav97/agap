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
    public class ExpensesControllerTests
    {
        private Mock<IGenericUnitOfWork<Expense>> _mockGenericUnitOfWork;
        private Mock<IExpensesUnitOfWork> _mockExpensesUnitOfWork;
        private ExpensesController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _mockGenericUnitOfWork = new Mock<IGenericUnitOfWork<Expense>>();
            _mockExpensesUnitOfWork = new Mock<IExpensesUnitOfWork>();
            _controller = new ExpensesController(_mockGenericUnitOfWork.Object, _mockExpensesUnitOfWork.Object);
        }

        [TestMethod]
        public async Task GetAsync_Pagination_ShouldReturnOk()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var expenses = new List<Expense>
            {
                new Expense { Id = 1, CropId = 1, ExpenseValue = 100.0f, ExpenseDescription = ExpenseType.LaborExpense, ExpenseDate = DateTime.Now },
                new Expense { Id = 2, CropId = 2, ExpenseValue = 200.0f, ExpenseDescription = ExpenseType.PesticideExpense, ExpenseDate = DateTime.Now }
            };
            var response = new Response<IEnumerable<Expense>> { WasSuccess = true, Result = expenses };
            _mockExpensesUnitOfWork.Setup(x => x.GetAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(expenses, okResult?.Value);
            _mockExpensesUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_Pagination_ShouldReturnBadRequest()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new Response<IEnumerable<Expense>> { WasSuccess = false };
            _mockExpensesUnitOfWork.Setup(x => x.GetAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockExpensesUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ShouldReturnOk()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var totalPages = 5;
            var response = new Response<int> { WasSuccess = true, Result = totalPages };
            _mockExpensesUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetPagesAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(totalPages, okResult?.Value);
            _mockExpensesUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ShouldReturnBadRequest()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new Response<int> { WasSuccess = false };
            _mockExpensesUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetPagesAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockExpensesUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once());
        }
    }
}

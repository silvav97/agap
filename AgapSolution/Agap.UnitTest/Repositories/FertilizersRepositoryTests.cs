using Agap.Backemd.Data;
using Agap.Backemd.Repositories;
using Agap.Shared.DTOs;
using Agap.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agap.UnitTest.Repositories
{
    [TestClass]
    public class FertilizersRepositoryTests
    {
        private DataContext _context = null!;
        private FertilizersRepository _repository = null!;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);
            _repository = new FertilizersRepository(_context);

            // Asegúrate de que todas las propiedades requeridas estén presentes
            _context.Fertilizers.AddRange(new List<Fertilizer>
    {
        new Fertilizer { Id = 1, Name = "Electronics", Brand = "BestFertilizer" },
        new Fertilizer { Id = 2, Name = "Books", Brand = "BestFertilizer" },
        new Fertilizer { Id = 3, Name = "Clothing", Brand = "BestFertilizer" },
    });

            _context.SaveChanges();
        }


        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetAsync_ReturnsFilteredFertilizers()
        {
            // Arrange
            var pagination = new PaginationDTO { Filter = "Book", RecordsNumber = 10, Page = 1 };

            // Act
            var response = await _repository.GetAsync(pagination);

            // Assert
            Assert.IsTrue(response.WasSuccess);
            var fertilizers = response.Result!.ToList();
            Assert.AreEqual(1, fertilizers.Count);
            Assert.AreEqual("Books", fertilizers.First().Name);
        }

        [TestMethod]
        public async Task GetAsync_ReturnsAllFertilizers_WhenNoFilterIsProvided()
        {
            // Arrange
            var pagination = new PaginationDTO { RecordsNumber = 10, Page = 1 };

            // Act
            var response = await _repository.GetAsync(pagination);

            // Assert
            Assert.IsTrue(response.WasSuccess);
            var fertilizers = response.Result!.ToList();
            Assert.AreEqual(3, fertilizers.Count);
        }

        [TestMethod]
        public async Task GetTotalPagesAsync_ReturnsCorrectNumberOfPages()
        {
            // Arrange
            var pagination = new PaginationDTO { RecordsNumber = 2, Page = 1 };

            // Act
            var response = await _repository.GetTotalPagesAsync(pagination);

            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.AreEqual(2, response.Result);
        }

        [TestMethod]
        public async Task GetTotalPagesAsync_WithFilter_ReturnsCorrectNumberOfPages()
        {
            // Arrange
            var pagination = new PaginationDTO { RecordsNumber = 2, Page = 1, Filter = "Bo" };

            // Act
            var response = await _repository.GetTotalPagesAsync(pagination);

            // Assert
            Assert.IsTrue(response.WasSuccess);
            Assert.AreEqual(1, response.Result);
        }
    }
}
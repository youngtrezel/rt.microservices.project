using Commercial.API.Handlers;
using Commercial.Domain.Models;
using Commercial.Domain.Models.Data;
using Commercial.Repository.Interfaces;
using Moq;


namespace Commercial.Tests.HandlerTests
{
    public class PlateHandlerTests
    {

        [Fact]
        public async Task GetPlateReturnsPlateObject()
        {
            // Arrange
            string registration = "WET1";
            Plate plate = new Plate();
            var mockRepo = new Mock<IPlateRepository>();
            mockRepo.Setup(repo => repo.GetPlate(registration))
                .ReturnsAsync(plate);

            var plateHandler = new PlatesHandler(mockRepo.Object);

            //Act
            var result = await plateHandler.GetPlate(registration);

            //Assert
            var returnResult = Assert.IsType<Plate>(result);
            Assert.NotNull(returnResult);
            Assert.Equal(registration, returnResult.Registration);
        }

        [Fact]
        public async Task PaginationPlatesReturns20Items()
        {
            // Arrange
            int pageNo = 1;
            int pageSize = 20;  
            Plate plate = new Plate();
            var mockRepo = new Mock<IPlateRepository>();
            mockRepo.Setup(repo => repo.GetPlates(pageNo, pageSize))
                .ReturnsAsync(new List<Plate>());

            var plateHandler = new PlatesHandler(mockRepo.Object);

            //Act
            var result = await plateHandler.GetPaginationPlates(pageNo, pageSize);

            //Assert
            var returnResult = Assert.IsType<List<Plate>>(result);
            Assert.NotNull(returnResult);
            Assert.Equal(20, returnResult.Count);
        }

        [Fact]
        public async Task UnreservedPlatesReturns20Items()
        {
            // Arrange
            int pageNo = 1;
            int pageSize = 20;
            Plate plate = new Plate();
            var mockRepo = new Mock<IPlateRepository>();
            mockRepo.Setup(repo => repo.GetPlates(pageNo, pageSize))
                .ReturnsAsync(new List<Plate>());

            var plateHandler = new PlatesHandler(mockRepo.Object);

            //Act
            var result = await plateHandler.GetUnreservedPlates(pageNo, pageSize);

            //Assert
            var returnResult = Assert.IsType<List<Plate>>(result);
            Assert.NotNull(returnResult);
            Assert.Equal(20, returnResult.Count);
        }

        [Fact]
        public async Task UnsoldPlatesReturns20Items()
        {
            // Arrange
            int pageNo = 1;
            int pageSize = 20;
            Plate plate = new Plate();
            var mockRepo = new Mock<IPlateRepository>();
            mockRepo.Setup(repo => repo.GetPlates(pageNo, pageSize))
                .ReturnsAsync(new List<Plate>());

            var plateHandler = new PlatesHandler(mockRepo.Object);

            //Act
            var result = await plateHandler.GetUnsoldPlates(pageNo, pageSize);

            //Assert
            var returnResult = Assert.IsType<List<Plate>>(result);
            Assert.NotNull(returnResult);
            Assert.Equal(20, returnResult.Count);
        }

        [Fact]
        public async Task AddPlateReturnsPlateObject()
        {
            // Arrange
            PlateDto dto = new PlateDto();
            Plate plate = new Plate();
            var mockRepo = new Mock<IPlateRepository>();
            mockRepo.Setup(repo => repo.AddPlate(dto))
                .ReturnsAsync(plate);

            var plateHandler = new PlatesHandler(mockRepo.Object);

            //Act
            var result = await plateHandler.AddPlate(dto);

            //Assert
            var returnResult = Assert.IsType<Plate>(result);
            Assert.NotNull(returnResult);

        }

    }
}

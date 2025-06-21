using Moq;
using YakShop.Core.Commands;
using YakShop.Core.Entities;
using YakShop.Core.Interfaces.Repository;
using YakShop.Core.Operation;
using YakShop.Core.Services;

namespace YakShop.Tests.Services
{
    public class ShopServiceTests
    {

        [Fact]
        public async Task GetHerd_ReturnsExpectedResult()
        {
            // Arrange
            var mockRepo = new Mock<IYakShopRepository>();
            var expectedHerd = new List<Yak>
            {
                new ("Yak-1", 4.0m, Core.Enumerations.Sex.FEMALE),
                new ("Yak-2", 8.0m, Core.Enumerations.Sex.FEMALE)
            };

            mockRepo.Setup(r => r.GetStock()).ReturnsAsync((0, 0m));
            mockRepo.Setup(r => r.GetHerd()).ReturnsAsync(expectedHerd);
            mockRepo.Setup(r => r.UpdateStock(It.IsAny<int>(), It.IsAny<decimal>())).Returns(Task.CompletedTask);
            mockRepo.Setup(r => r.UpdateHerd(It.IsAny<List<Yak>>())).Returns(Task.CompletedTask);

            var service = new ShopService(mockRepo.Object);

            // Act
            var result = await service.GetHerd(1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(OperationStatus.Ok, result.OperationStatus);
            Assert.Equal(expectedHerd.Count, result.Data.Count);
            Assert.Equal("Yak-1", result.Data[0].Name);
        }

        [Fact]
        public async Task Order_WithEnoughStock_ReturnsCreatedStatus()
        {
            // Arrange
            var mockRepo = new Mock<IYakShopRepository>();

            // Simulate initial stock
            mockRepo.Setup(r => r.GetStock()).ReturnsAsync((skins: 10, milk: 20m));
            mockRepo.Setup(r => r.UpdateStock(It.IsAny<int>(), It.IsAny<decimal>())).Returns(Task.CompletedTask);

            var service = new ShopService(mockRepo.Object);

            var orderCommand = new OrderCommand
            {
                Order = new Items
                {
                    Skins = 5,
                    Milk = 10m
                }
            };

            // Act
            var result = await service.Order(0, orderCommand);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(OperationStatus.Created, result.OperationStatus);
            Assert.Equal(5, result.Data.Skins);
            Assert.Equal(10m, result.Data.Milk);

            mockRepo.Verify(r => r.UpdateStock(5, 10m), Times.Once);
        }

        [Fact]
        public async Task Order_WithNotEnoughStock_ReturnsPartialStatus()
        {
            // Arrange
            var mockRepo = new Mock<IYakShopRepository>();

            // Only 3 skins and 8 milk available
            mockRepo.Setup(r => r.GetStock()).ReturnsAsync((skins: 6, milk: 8m));
            mockRepo.Setup(r => r.UpdateStock(It.IsAny<int>(), It.IsAny<decimal>())).Returns(Task.CompletedTask);

            var service = new ShopService(mockRepo.Object);

            var orderCommand = new OrderCommand
            {
                Order = new Items
                {
                    Skins = 5,
                    Milk = 10m
                }
            };

            // Act
            var result = await service.Order(0, orderCommand);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(OperationStatus.Partial, result.OperationStatus);
            Assert.Equal(5 ,result.Data.Skins);  // Because requested skins > stock
            Assert.Null(result.Data.Milk);    // Because requested milk > stock

            mockRepo.Verify(r => r.UpdateStock(1, 8m), Times.Once);
        }
    }
}

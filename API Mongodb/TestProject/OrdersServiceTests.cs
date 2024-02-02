using Xunit;
using Moq;
using SkiServiceManagementApi.Models;
using SkiServiceManagementApi.Services;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

public class OrdersServiceTests
{
    private readonly Mock<IMongoCollection<Order>> _mockOrdersCollection;
    private readonly Mock<IMongoClient> _mockMongoClient;
    private readonly Mock<IMongoDatabase> _mockMongoDatabase;
    private readonly SkiServiceManagementService _service;

    public OrdersServiceTests()
    {
        _mockOrdersCollection = new Mock<IMongoCollection<Order>>();
        _mockMongoClient = new Mock<IMongoClient>();
        _mockMongoDatabase = new Mock<IMongoDatabase>();
        var mockSettings = new Mock<IOptions<SkiServiceDatabaseSettings>>();

        mockSettings.Setup(s => s.Value).Returns(new SkiServiceDatabaseSettings
        {
            DatabaseName = "TestDB",
            OrdersCollectionName = "Orders"
        });

        _mockMongoClient.Setup(c => c.GetDatabase(It.IsAny<string>(), null))
                        .Returns(_mockMongoDatabase.Object);

        _mockMongoDatabase.Setup(db => db.GetCollection<Order>(It.IsAny<string>(), null))
                          .Returns(_mockOrdersCollection.Object);

        _service = new SkiServiceManagementService(mockSettings.Object);
    }

    [Fact]
    public async Task GetOrdersAsync_ReturnsAllOrders()
    {
        // Arrange
        var fakeOrders = new List<Order>
        {
            new Order { /* ... Properties ... */ },
            new Order { /* ... Properties ... */ }
        };
        var mockCursor = new Mock<IAsyncCursor<Order>>();
        mockCursor.SetupSequence(c => c.MoveNext(It.IsAny<CancellationToken>()))
                  .Returns(true)
                  .Returns(false);
        mockCursor.Setup(c => c.Current).Returns(fakeOrders);

        _mockOrdersCollection.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<Order>>(),
            It.IsAny<FindOptions<Order>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);

        // Act
        var result = await _service.GetOrdersAsync();

        // Assert
        Assert.Equal(fakeOrders.Count, result.Count);
    }

    [Fact]
    public async Task CreateOrderAsync_CreatesOrder()
    {
        // Arrange
        var fakeOrder = new Order { /* Initialisieren Sie das Order-Objekt */ };
        _mockOrdersCollection.Setup(x => x.InsertOneAsync(It.IsAny<Order>(), null, default))
            .Returns(Task.CompletedTask);

        // Act
        await _service.CreateOrderAsync(fakeOrder);

        // Assert
        _mockOrdersCollection.Verify(x => x.InsertOneAsync(It.IsAny<Order>(), null, default), Times.Once);
    }

    [Fact]
    public async Task UpdateOrderAsync_UpdatesOrder()
    {
        // Arrange
        var fakeOrderId = "someOrderId";
        var fakeOrder = new Order { /* Initialisieren Sie das Order-Objekt mit fakeOrderId als ID */ };
        _mockOrdersCollection.Setup(x => x.ReplaceOneAsync(It.IsAny<FilterDefinition<Order>>(),
            It.IsAny<Order>(), It.IsAny<ReplaceOptions>(), default))
            .ReturnsAsync(new ReplaceOneResult.Acknowledged(1, 1, fakeOrderId));

        // Act
        await _service.UpdateOrderAsync(fakeOrderId, fakeOrder);

        // Assert
        _mockOrdersCollection.Verify(x => x.ReplaceOneAsync(It.IsAny<FilterDefinition<Order>>(),
            It.IsAny<Order>(), It.IsAny<ReplaceOptions>(), default), Times.Once);
    }

    [Fact]
    public async Task RemoveOrderAsync_RemovesOrder()
    {
        // Arrange
        var fakeOrderId = "someOrderId";
        _mockOrdersCollection.Setup(x => x.DeleteOneAsync(It.IsAny<FilterDefinition<Order>>(), default))
            .ReturnsAsync(new DeleteResult.Acknowledged(1));

        // Act
        await _service.RemoveOrderAsync(fakeOrderId);

        // Assert
        _mockOrdersCollection.Verify(x => x.DeleteOneAsync(It.IsAny<FilterDefinition<Order>>(), default), Times.Once);
    }
}

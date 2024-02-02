using SkiServiceManagementApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkiServiceManagementApi.Services;

public class SkiServiceManagementService
{
    private readonly IMongoCollection<Order> _ordersCollection;
    private readonly IMongoCollection<Employee> _employeesCollection;

    public SkiServiceManagementService(
        IOptions<SkiServiceDatabaseSettings> skiServiceDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            skiServiceDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            skiServiceDatabaseSettings.Value.DatabaseName);

        _ordersCollection = mongoDatabase.GetCollection<Order>(
            skiServiceDatabaseSettings.Value.OrdersCollectionName);

        _employeesCollection = mongoDatabase.GetCollection<Employee>(
            skiServiceDatabaseSettings.Value.EmployeesCollectionName);
    }

    // Methoden für Orders
    public async Task<List<Order>> GetOrdersAsync() =>
        await _ordersCollection.Find(_ => true).ToListAsync();

    public async Task<Order?> GetOrderAsync(string id) =>
        await _ordersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateOrderAsync(Order newOrder) =>
        await _ordersCollection.InsertOneAsync(newOrder);

    public async Task UpdateOrderAsync(string id, Order updatedOrder) =>
        await _ordersCollection.ReplaceOneAsync(x => x.Id == id, updatedOrder);

    public async Task RemoveOrderAsync(string id) =>
        await _ordersCollection.DeleteOneAsync(x => x.Id == id);

    // Methoden für Employees
    public async Task<List<Employee>> GetEmployeesAsync() =>
        await _employeesCollection.Find(_ => true).ToListAsync();

    public async Task<Employee?> GetEmployeeAsync(string id) =>
        await _employeesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateEmployeeAsync(Employee newEmployee) =>
        await _employeesCollection.InsertOneAsync(newEmployee);

    public async Task UpdateEmployeeAsync(string id, Employee updatedEmployee) =>
        await _employeesCollection.ReplaceOneAsync(x => x.Id == id, updatedEmployee);

    public async Task RemoveEmployeeAsync(string id) =>
        await _employeesCollection.DeleteOneAsync(x => x.Id == id);
}

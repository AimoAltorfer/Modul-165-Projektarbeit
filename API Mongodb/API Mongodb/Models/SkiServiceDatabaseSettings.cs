namespace SkiServiceManagementApi.Models;

public class SkiServiceDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string OrdersCollectionName { get; set; } = null!;

    public string EmployeesCollectionName { get; set; } = null!;
}

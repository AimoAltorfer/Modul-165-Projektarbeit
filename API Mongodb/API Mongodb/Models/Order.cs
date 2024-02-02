using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SkiServiceManagementApi.Models;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("CustomerName")]
    public string CustomerName { get; set; } = null!;

    [BsonElement("CustomerEmail")]
    public string CustomerEmail { get; set; } = null!;

    [BsonElement("CustomerPhone")]
    public string CustomerPhone { get; set; } = null!;

    [BsonElement("Priority")]
    public string Priority { get; set; } = null!;

    [BsonElement("ServiceType")]
    public string ServiceType { get; set; } = null!;

    [BsonElement("CreateDate")]
    public DateTime CreateDate { get; set; }

    [BsonElement("PickupDate")]
    public DateTime PickupDate { get; set; }

    [BsonElement("Status")]
    public string Status { get; set; } = null!;

    [BsonElement("Comment")]
    public string Comment { get; set; } = null!;
}

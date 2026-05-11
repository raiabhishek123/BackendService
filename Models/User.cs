using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend_service.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("firstName")]
    public string? FirstName { get; set; }
    [BsonElement("lastName")]
    public string? LastName { get; set; }
    [BsonElement("phoneNumber")]
    public string? PhoneNumber { get; set; }
    [BsonElement("password")]
    public string? Password { get; set; }
    [BsonElement("roles")]
    public List<string> Roles { get; set; }
}
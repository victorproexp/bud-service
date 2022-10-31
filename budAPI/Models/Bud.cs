using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace budAPI.Models;

public class Bud
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? KundeId { get; set; }

    public int Value { get; set; }
}

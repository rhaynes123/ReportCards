using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReportCardz.Models;

public class Child
{
    public ObjectId Id { get; set; }
    [BsonElement("name")]
    public required string Name { get; set; }
    [BsonElement("courses")]
    public IDictionary<string, char> Courses { get; set; } = new Dictionary<string, char>();
    [Timestamp]
    public long Version { get; set; }
    [ConcurrencyCheck]
    public DateTimeOffset LastModified { get; set; } = DateTimeOffset.UtcNow;
}


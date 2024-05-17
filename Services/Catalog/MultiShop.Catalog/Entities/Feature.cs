using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiShop.Catalog.Entities;

public class Feature
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)] //benzersiz olması için 
    public string FeatureId { get; set; }
    public string Title { get; set; }
    public string Icon { get; set; }
}

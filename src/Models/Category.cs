using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace to_do_list.src.Models
{
    public class Category : ModelBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        
        [BsonElement("name")]
        public string Name {get;set;} = string.Empty;

        [BsonElement("code")]
        public string Code {get;set;} = string.Empty;
    }
}
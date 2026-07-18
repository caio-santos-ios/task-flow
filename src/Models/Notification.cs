using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace to_do_list.src.Models
{
    public class Notification : ModelBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("read")]
        public bool Read { get; set; } = false;

        [BsonElement("send")]
        public bool Send { get; set; } = false;

        [BsonElement("sendBy")]
        public string SendBy { get; set; } = string.Empty;

        [BsonElement("sendAt")]
        public DateTime SendAt { get; set; }

        [BsonElement("link")]
        public string Link { get; set; } = string.Empty;

        [BsonElement("icon")]
        public string Icon { get; set; } = string.Empty;
    }
}
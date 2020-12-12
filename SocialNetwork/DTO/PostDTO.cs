using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PostDTO
    {
        
        [BsonId]
        public string PostID { get; set; }
        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("username")]
        public string UserID { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }
        [BsonIgnoreIfNull]
        [BsonElement("like")]
        public List<LikeDTO> Like { get; set; }
        [BsonElement("date")]
        public DateTime Date { get; set; }
        [BsonIgnoreIfNull]
        [BsonElement("comments")]
        public List<CommentDTO> Comments { get; set; }

    }
}

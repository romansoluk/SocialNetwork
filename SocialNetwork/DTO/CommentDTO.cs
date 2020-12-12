using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CommentDTO
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string CommentID { get; set; }
        [BsonElement("userID")]
        public string UserID { get; set; }
        [BsonElement("postID")]
        public string PostID { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }
    }
}

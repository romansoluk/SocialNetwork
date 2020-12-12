using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class LikeDTO
    {
        [BsonId]
        public string PostID;
        [BsonElement("username")]
        public List<string> Username { get; set; }
        [BsonIgnoreIfNull]
        [BsonElement("like")]
        public int like { get; set; }
    }
}

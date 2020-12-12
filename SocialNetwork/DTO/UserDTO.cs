using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace DTO
{
    public class UserDTO
    {
        //[BsonElement("first name")]
        //public string FirstName { get; set; }

        //[BsonElement("last name")]
        //public string LastName { get; set; }

        [BsonId]
        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("description")]
        [BsonIgnoreIfNull]
        public string Description { get; set; }

        [BsonElement("posts")]
        [BsonIgnoreIfNull]
        public List<PostDTO> Posts { get; set; }

        [BsonElement("following")]
        [BsonIgnoreIfNull]
        public List<string> Following { get; set; }

        [BsonElement("followers")]
        [BsonIgnoreIfNull]
        public List<string> Followers { get; set; }


    }
}

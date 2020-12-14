using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTONeo4j
{
    public class UserDTONeo4j
     {

        public UserDTONeo4j() { }
        public UserDTONeo4j(string username, List<string> followers, List<string> following)
        {
            Username = username;
            Followers = followers;
            Following = following;
        }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
        [JsonProperty(PropertyName = "Followers")]
        public List<string> Followers { get; set; }

        [JsonProperty(PropertyName = "Following")]
        public List<string> Following { get; set; }
           
    }
}

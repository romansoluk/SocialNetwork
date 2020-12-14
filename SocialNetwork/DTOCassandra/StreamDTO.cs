using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;

namespace DTOCassandra
{
    public class StreamDTO
    {
        public StreamDTO() { }

        public StreamDTO(string _username, List<PostDTOCassandra> _posts,  List<string> _Followers)
        {
            username = _username;
            posts = _posts;
            Followers = _Followers;
        }


        public string username;
        public List<PostDTOCassandra> posts;
        public List<string> Followers;


    }
}

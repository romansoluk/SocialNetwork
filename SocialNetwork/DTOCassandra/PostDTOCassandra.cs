using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOCassandra
{
    public class PostDTOCassandra
    {
        public string PostID;
        public string username;
        public string Text;
        public DateTime Date;
        public List<CommentDTOCassandra> Comments;
    }
}

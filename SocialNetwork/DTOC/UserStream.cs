using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOC
{
    public class UserStream
    {
        public Guid UserID { get; set; }
        public Guid PostID { get; set; }
        public string PostText { get; set; }
        public List<CommentC> Comments { get; set; }
        public TimeUuid UpdTime { get; set; }
    }
}

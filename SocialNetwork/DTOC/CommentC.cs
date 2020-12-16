using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOC
{
    public class CommentC
    {
       public Guid CommentID { get; set; }
        public Guid PostID { get; set; }
        public Guid UserID { get; set; }
        public string Text { get; set; }
        public TimeUuid UpdTime { get; set; }

    }
}

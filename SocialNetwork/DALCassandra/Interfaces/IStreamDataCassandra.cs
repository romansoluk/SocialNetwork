using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALCassandra.Concrete;
using DTOCassandra;

namespace DALCassandra.Interfaces
{
    public interface IStreamDataCassandra
    {
        void CreatePost(PostDTOCassandra post, string username);
        void CreateComment(PostDTOCassandra post, CommentDTOCassandra comment, string username);
       void UpdatePost(PostDTOCassandra post, string username);
    }
}

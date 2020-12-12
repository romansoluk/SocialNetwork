using DAL.Concrete;
using DTO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
   public interface ICommentDAL 
    {

        void createComment(CommentDTO comment, string PostID);
        void deleteComment(CommentDTO comment, string PostID);
        List<CommentDTO> showComments(string PostID);
        
    }
}

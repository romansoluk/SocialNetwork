using DTO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Interfaces
{
    public interface ILikeDAL
    { 
        bool checkLike(string UserID, string PostID);
        bool setLike(PostDTO post, string UserID, LikeDTO like);
        bool dislike(PostDTO post, string UserID, LikeDTO like);
   
    }
}

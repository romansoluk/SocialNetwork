using DTO;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPostDAL
    {

        void createPost(PostDTO post, string username);
        void deletePost(PostDTO post, string username);
        List<PostDTO> showPosts(string username);
        List<PostDTO> Newsfeed(string username);
       
       


    }
}

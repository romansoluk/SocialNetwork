using DAL.Interfaces;
using DTO;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class PostDAL : IPostDAL
    {
        private string connectionString;
        IMongoCollection<PostDTO> Posts;
        IMongoCollection<UserDTO> Users;

        public PostDAL(string connectionString)
        {
            this.connectionString = connectionString;
            var connection = new MongoUrlBuilder(connectionString);
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(connection.DatabaseName);
            Posts = database.GetCollection<PostDTO>("Post");
            Users = database.GetCollection<UserDTO>("User");
        }



        public void createPost(PostDTO post, string username)
        {
            UserDTO user = Users.Find(Builders<UserDTO>.Filter.Eq("_id", username)).First();
            user.Posts.Add(post);
            Posts.InsertOne(post);
            Users.UpdateOne(Builders<UserDTO>.Filter.Eq("_id", username), Builders<UserDTO>.Update.Set("Posts", user.Posts));

        }

        public void deletePost(PostDTO post, string username)
        {
            UserDTO user = Users.Find(Builders<UserDTO>.Filter.Eq("_id", username)).First();
            user.Posts.Remove(post);
            Posts.DeleteOne(Builders<PostDTO>.Filter.Eq("_id", post.PostID));
            Users.UpdateOne(Builders<UserDTO>.Filter.Eq("_id", username), Builders<UserDTO>.Update.Set("Posts", user.Posts));
        }

        public List<PostDTO> showPosts(string username)
        {
            
            List<PostDTO> posts = Users.Find(Builders<UserDTO>.Filter.Eq("_id", username)).First().Posts;   
            return posts;

        }


        public List<PostDTO> Newsfeed(string username)
        {

            UserDTO user = Users.Find(Builders<UserDTO>.Filter.Eq("_id", username)).First();
            List<string> users = user.Following;
            List<PostDTO> posts = new List<PostDTO>();
            foreach(var user1 in users)
            {
                List<PostDTO> posts1 = Users.Find(Builders<UserDTO>.Filter.Eq("_id", user1)).First().Posts;
                foreach(var post in posts1)
                {

                    posts1.Add(post);
                }
            }
            
            return posts;

        }





    }
}

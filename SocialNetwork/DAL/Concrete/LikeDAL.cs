using DAL.Interfaces;
using DTO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Concrete
{
    public class LikeDAL :ILikeDAL
    {
        private string connectionString;
        IMongoCollection<LikeDTO> Likes;
        IMongoCollection<PostDTO> Posts;

        public LikeDAL(string connectionString)
        {
            this.connectionString = connectionString;
            var connection = new MongoUrlBuilder(connectionString);
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(connection.DatabaseName);
            Likes = database.GetCollection<LikeDTO>("Like");
            Posts = database.GetCollection<PostDTO>("Post");
        }


        public bool checkLike(string UserID, string PostID)
       {
            //List<LikeDTO> likes = Posts.Find(Builders<PostDTO>.Filter.Eq("_id", PostID)).First().Like;
            //LikeDTO userLike
            List<string> usersLiked = Likes.Find(Builders<LikeDTO>.Filter.Eq("_id", PostID)).First().Username;
            if (usersLiked.Contains(UserID))
            {
                return true;
            }
            else return false;
       }


        public bool setLike(PostDTO post, string UserID, LikeDTO like)
        {
            if(checkLike(UserID, post.PostID.ToString())!=true)
            {
                like.Username.Add(UserID);
                like.like++;
                Likes.UpdateOne(Builders<LikeDTO>.Filter.Eq("_id", post.PostID), Builders<LikeDTO>.Update.Set("Username", like.Username));
                Likes.UpdateOne(Builders<LikeDTO>.Filter.Eq("_id", post.PostID), Builders<LikeDTO>.Update.Set("like", like.like));
                return true;
            }

            return true; //in case it was liked
            
        }

        public bool dislike(PostDTO post, string UserID, LikeDTO like)
        {
            if (checkLike(UserID, post.PostID.ToString()) == true)
            {
                like.Username.Remove(UserID);
                like.like--;
                Likes.UpdateOne(Builders<LikeDTO>.Filter.Eq("_id", post.PostID), Builders<LikeDTO>.Update.Set("Username", like.Username));
                Likes.UpdateOne(Builders<LikeDTO>.Filter.Eq("_id", post.PostID), Builders<LikeDTO>.Update.Set("like", like.like));
                return true;
            }

            return true; //in case it was not liked

        }



    }
}

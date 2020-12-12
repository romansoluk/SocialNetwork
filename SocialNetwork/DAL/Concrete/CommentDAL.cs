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
   public class CommentDAL : ICommentDAL
    {
        private string connectionString;
        IMongoCollection<CommentDTO> Comments;
        IMongoCollection<PostDTO> Posts;

        public CommentDAL(string connectionString)
        {
            this.connectionString = connectionString;
            var connection = new MongoUrlBuilder(connectionString);
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(connection.DatabaseName);
            Comments = database.GetCollection<CommentDTO>("Comment");
            Posts = database.GetCollection<PostDTO>("Post");
        }


        public void createComment(CommentDTO comment, string PostID)
        {
            PostDTO post = Posts.Find(Builders<PostDTO>.Filter.Eq("_id", PostID)).First();
            post.Comments.Add(comment);
            Comments.InsertOne(comment);
            Posts.UpdateOne(Builders<PostDTO>.Filter.Eq("_id", PostID), Builders<PostDTO>.Update.Set("Comments", post.Comments));
        }

        public void deleteComment(CommentDTO comment, string PostID)
        {
            PostDTO post = Posts.Find(Builders<PostDTO>.Filter.Eq("_id", PostID)).First();
            post.Comments.Remove(comment);
            Comments.DeleteOne(Builders<CommentDTO>.Filter.Eq("_id", comment.CommentID));
            Posts.UpdateOne(Builders<PostDTO>.Filter.Eq("_id", PostID), Builders<PostDTO>.Update.Set("Comments", post.Comments));
        }


        public List<CommentDTO> showComments(string PostID)
        {
            List<CommentDTO> comments = Posts.Find(Builders<PostDTO>.Filter.Eq("_id", PostID)).First().Comments;
            return comments;
        }
    }
}

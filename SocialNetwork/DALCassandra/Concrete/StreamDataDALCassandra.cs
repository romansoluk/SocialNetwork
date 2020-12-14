using Cassandra;
using DALCassandra.Interfaces;
using DTOCassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALCassandra.Concrete
{
     

    public class StreamDataDALCassandra
    {
        private readonly Cluster _cluster;
        private readonly string[] _nodes;
        private readonly string _KeySpace;
        IStreamDALCassandra _stream;

        public StreamDataDALCassandra(string KeySpace, string[] nodes, IStreamDALCassandra stream)
        {
            _nodes = nodes;
            _KeySpace = KeySpace;
            _stream = stream;
            ConsistencyLevel consistency = ConsistencyLevel.One;
            _cluster = Cluster.Builder()
                .AddContactPoints(nodes)
                .WithQueryOptions(new QueryOptions().SetConsistencyLevel(consistency))
                .Build();

        }

        public void CreatePost(PostDTOCassandra post, string username)
        {
            var session = _cluster.Connect();
            var newPost = session.Prepare("INSERT INTO posts (PostID, Username ,Text, Date) VALUES(?,?,?, ?)");
            session.Execute(newPost.Bind(post.PostID, username, post.Text, DateTime.Now));
            _stream.SynchronizeStream(session, post);
        }


        public void CreateComment(PostDTOCassandra post, CommentDTOCassandra comment, string username)
        {


            
            var session = _cluster.Connect();


            var addComment = session.Prepare(
                "INSERT INTO comments (PostID, CommentID, Text, UserID) VALUES(?,?,?, ?)");
            session.Execute(addComment.Bind(comment.PostID, comment.CommentID, comment.Text, comment.UserID));

            var updatedPost = session.Prepare(
                "Update posts set Date=?  where PostID = ?");
            session.Execute(updatedPost.Bind(comment.CommentID, comment.PostID));

           
           
           

            _stream.SynchronizeStream(session, post);
        }

        public void UpdatePost(PostDTOCassandra post, string username)
        {
            var session = _cluster.Connect();
   
            var getDate = session.Prepare("Select Date from posts where PostID = ? ");
          
            var updatedPost = session.Prepare(
                "Update posts set Text = ?, Date=?  where PostID = ?");
            session.Execute(updatedPost.Bind(post.Text, DateTime.Now));

            _stream.SynchronizeStream(session, post);
        }
    }
}

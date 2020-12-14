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
    public class StreamDALCassandra : IStreamDALCassandra
    {
        private readonly Cluster _cluster;
        private readonly string[] _nodes;
        private readonly string _KeySpace;


        public StreamDALCassandra(string KeySpace, string[] nodes)
        {
            _nodes = nodes;
            _KeySpace = KeySpace;
            ConsistencyLevel consistency = ConsistencyLevel.One;
            _cluster = Cluster.Builder()
                .AddContactPoints(nodes)
                .WithQueryOptions(new QueryOptions().SetConsistencyLevel(consistency))
                .Build();

        }


        public void SynchronizeStream(ISession session, PostDTOCassandra post)
        {

           session = _cluster.Connect();
           List<string> list = new List<string>();
           List<CommentDTOCassandra> comments = new List<CommentDTOCassandra>();


           GetFollwers( post, session, list);
           DeletePost(post, session, list);
           GetPosts(post, session, list);
           GetComments(comments, post.PostID, session, list);
            AddTostream(session, post, list, comments);
        }


        private void GetFollwers(PostDTOCassandra post, ISession session, List<string> list)
        {
            
            var getFollowerList = session.Prepare("Select followers from Followers where PostID = ? ");
            var Followers = session.Execute(getFollowerList.Bind(post.PostID));

            list = new List<string>();

            foreach (var follower in Followers)
            {
                list = follower.GetValue<List<string>>("followers");

            }
        }


        private void DeletePost(PostDTOCassandra post, ISession session, List<string> list)
        {
            var deletePost = session.Prepare("DELETE FROM user_stream where user_id =? and last_updated_at = ?");
            foreach (var follower in list)
            {
                session.Execute(deletePost.Bind(follower, DateTime.Now));
            }
        }


        public void GetPosts(PostDTOCassandra post, ISession session, List<string> list)
        {
          

            var getPosts = session.Prepare("Select * from posts where Username = ? ");
            var postsList = session.Execute(getPosts.Bind(post.username)).ToList();

            foreach (var post1 in postsList)
            {
                post.username = post1.GetValue<string>("Username");
                post.Date= post1.GetValue<DateTime>("Date");
                post.Text = post1.GetValue<string>("Text");
            }
        }


        public void GetComments(List<CommentDTOCassandra> comments, string PostID, ISession session, List<string> list)
        {
            comments = new List<CommentDTOCassandra>();
            var getComments = session.Prepare("Select * from comments where PostID = ? ");
            var Comments = session.Execute(getComments.Bind(PostID));

            foreach (var comment1 in  Comments)
            {
                CommentDTOCassandra comment = new CommentDTOCassandra()
                {
                   CommentID = comment1.GetValue<string>("CommentID"),
                   PostID = comment1.GetValue<string>("PostID"),
                   UserID  = comment1.GetValue<string>("UserID"),
                   Text = comment1.GetValue<string>("Text"),
                   Date= comment1.GetValue<DateTime>("Date")
                };

                comments.Add(comment);
            }
        }


        private void AddTostream(ISession session, PostDTOCassandra post, List<string> list, List<CommentDTOCassandra> comments)
        {
            var Stream =
                session.Prepare("INSERT INTO user_stream (Username, Date , PostID , UserID , Text, Comments) VALUES(?,?,?,?,?,?)");

            foreach (var follower in list)
            {
                session.Execute(Stream.Bind(follower, DateTime.Now, post.PostID,  post.username, post.Text, comments));
            }
        }
     

    }
}

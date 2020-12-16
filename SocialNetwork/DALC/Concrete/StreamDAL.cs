using Cassandra;
using DALC.Interfaces;
using DTOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALC.Concrete
{
    public class StreamDAL : IStreamDAL
    {
        private readonly Cluster _cluster;
        private readonly string[] _nodes;
        private readonly string _KeySpace;


        public StreamDAL(string KeySpace, string[] nodes)
        {
            _nodes = nodes;
            _KeySpace = KeySpace;
            ConsistencyLevel consistency = ConsistencyLevel.One;
            _cluster = Cluster.Builder()
                .AddContactPoints(nodes)
                .WithQueryOptions(new QueryOptions().SetConsistencyLevel(consistency))
                .Build();

        }


        public void SynchronizeStream(ISession session, PostC post)
        {
            // Get follower list
            var getFollowerList = session.Prepare("Select followers from PostFollowers where PostID = ? ");
            var listFollowers = session.Execute(getFollowerList.Bind(post.PostID));

            List<string> Followers = new List<string>();
            foreach (var follower in listFollowers)
            {
                Followers = follower.GetValue<List<string>>("followers");

            }

            //Delete post from user stream
            var deletePost = session.Prepare("DELETE FROM UserStream where UserId =? and PostID = ?");
            foreach (var follower in Followers)
            {
                session.Execute(deletePost.Bind(follower, post.PostID));
            }


         
            

            var getPosts = session.Prepare("Select * from Post where PostID = ? ");
            var postsList = session.Execute(getPosts.Bind(post.PostID));

            foreach (var post1 in postsList)
            {
                post.UserID = post1.GetValue<Guid>("UserID");
                post.UpdTime = post1.GetValue<TimeUuid>("UpdTime");
                post.Text= post1.GetValue<string>("Text");
               
            }



            //Get data from comments
            List<CommentC> comments = new List<CommentC>();
            var getComments = session.Prepare("Select comments from Post where PostID = ? ");
            var listComments = session.Execute(getComments.Bind(post.PostID));

            foreach (var commentRow in listComments)
            {
                CommentC comment = new CommentC()
                {
                    CommentID = commentRow.GetValue<Guid>("CommentID"),
                    UserID = commentRow.GetValue<Guid>("UserID"),
                    Text = commentRow.GetValue<string>("Text"),
                    UpdTime = commentRow.GetValue<TimeUuid>("UpdTime")
                };

                comments.Add(comment);
            }


            
           
            //Insert post in user stream
            var insertUserStream =
                session.Prepare("INSERT INTO UserStream (UserID, PostID,  Text, comments, UpdTime) VALUES(?,?,?,?,?,?)");

            foreach (var follower in Followers)
            {
                session.Execute(insertUserStream.Bind(follower, post.PostID, post.Text, comments, post.UpdTime));
            }

        }

    }

    }

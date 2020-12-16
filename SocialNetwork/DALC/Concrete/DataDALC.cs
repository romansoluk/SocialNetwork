using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOC;
using DALC.Interfaces;

namespace DALC.Concrete
{
   public  class DataDALC
    {
        private readonly Cluster _cluster;
        private readonly string[] _nodes;
        private readonly string _KeySpace;
        IStreamDAL _stream;

        public DataDALC(string KeySpace, string[] nodes, IStreamDAL stream)
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

        public void CreatePost(ISession session, string Text, Guid UserID, List<string> Followers)
        {

            Guid postID = Guid.NewGuid();
           
            TimeUuid UpdTime = TimeUuid.NewId(DateTimeOffset.Now);

            


            var insertPost = session.Prepare(
                "INSERT INTO Post (PostID, UserID, Text, UpdTime) VALUES(?,?,?, ?)");
            session.Execute(insertPost.Bind(postID, UserID, Text, UpdTime));

      

            var insertPostFollowers = session.Prepare(
                "INSERT INTO post_followers (PostID, followers) VALUES(?,?)");
            session.Execute(insertPostFollowers.Bind(postID, Followers));

            PostC post = new PostC
            {
                PostID = postID,
                UserID = UserID,
                Text = Text,
                UpdTime = UpdTime
            }; 

            _stream.SynchronizeStream(session, post);


        }
        public void UpdatePost(ISession session, PostC post)
        {

            TimeUuid UpdTime = TimeUuid.NewId(DateTimeOffset.Now);


            var updatedPost = session.Prepare(
                "Update Post set Text = ?, UpdTime=?  where PostID = ?");
            session.Execute(updatedPost.Bind(post.Text, UpdTime, post.PostID));


            _stream.SynchronizeStream(session, post);

        }



        public void CreateComment(ISession session, PostC post, string Text, Guid UserID)
        {
          
            TimeUuid UpdTime = TimeUuid.NewId(DateTimeOffset.Now);

            Guid CommentID = new Guid();
           

           

            var addComment = session.Prepare(
                "INSERT INTO comments (CommentID, PostID, UserID , Text, UpdTime) VALUES(?,?,?, ?)");
            session.Execute(addComment.Bind(CommentID, post.PostID, UserID, Text, UpdTime));

            var updatedPost = session.Prepare(
                "Update Post set UpdTime=?  where PostID = ?");
            session.Execute(updatedPost.Bind(UpdTime, post.PostID));

          

            _stream.SynchronizeStream(session, post);


        }
    }
}

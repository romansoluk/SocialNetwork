using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using DAL.Concrete;
using DAL.Interfaces;
using System.Web.UI.WebControls;
using Neo4jClient;
using DALNeo4j.Concrete;
using DALNeo4j.Interfaces;
using DTONeo4j;
using BL.Interface;
using BL.Concrete;
using Cassandra;
using DALCassandra.Interfaces;
using DALCassandra.Concrete;

namespace SocialNetwork
{
    class Program
    {

     


static void Main(string[] args)
            {

            string con = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            IUserDALNeo4j neo4J = new UserDALNeo4j("http://localhost:7474/db/data", "Social Network", "1");
            IUserDAL userDAL = new UserDAL(con);
            IPostDAL postDAL = new PostDAL(con);
            ICommentDAL commentDAL = new CommentDAL(con);
            ILikeDAL likeDAL = new LikeDAL(con);

            string[] nodes = { "127.0.0.1" };
             IStreamDALCassandra stream = new StreamDALCassandra("social_network", nodes);
            
            IStreamDataCassandra streamData = new StreamDataDALCassandra("social_network", nodes, stream1);

       

        IUser user = new User(userDAL, postDAL, commentDAL, likeDAL, neo4J, stream, streamData);

            //PostDTO post1 = new PostDTO
            //{
            //    PostID = u.Username + " " + DateTime.Now.ToString(),
            //    UserID = u.Username,
            //    Text = "And this is my first post?!",
            //    Date = DateTime.Now,
            //    Like = new List<LikeDTO>(),
            //    Comments = new List<CommentDTO>()
            //};
            //Console.WriteLine( post1.ToBsonDocument().GetElement(0));

            //user.SynchronizeStream(post1, session);

//            user.SynchronizeNewPost(post1, session);




            //UserDTO u = new UserDTO
            //{
            //    Username = "joe",
            //    Password = "1234",
            //    Followers = new List<string>(),
            //    Following = new List<string>(),
            //    Posts = new List<PostDTO>()

            //};

            //UserDTO u1 = new UserDTO
            //{
            //    Username = "liam",
            //    Password = "4321",
            //    Followers = new List<string>(),
            //    Following = new List<string>(),
            //    Posts = new List<PostDTO>()



                // };

                // UserDTO u2 = new UserDTO
                // {
                //     Username = "julie",
                //     Password = "4567",
                //     Followers = new List<string>(),
                //     Following = new List<string>(),
                //     Posts = new List<PostDTO>()

                // };

                //  user.AddUser(u);
                //  user.AddUser(u1);


                // user.FollowUser(u, u2);

                // user.RelationshipStatus(u, u2);

                // user.UnfollowUser(u, u2);

                // //user.CreateUser(u2);
                // //user.UpdateUserPassword(u);
                // //user.FollowUser(u, u1);
                // //user.FollowUser(u, u2);

                // //    //GetDatabaseNames(client).GetAwaiter();
                // //    Console.ReadLine();

                // // Console.WriteLine(user.LoginUser("joe", "1234"));






            ////postDAL.createPost(post1, u.Username);
            // Console.WriteLine(post1.PostID);

            // CommentDTO c1 = new CommentDTO
            // {
            //     PostID = post1.PostID.ToString(),
            //     UserID = u2.Username,
            //     Text = "Hello here"

            // };

            // //commentDAL.createComment(c1, post1.PostID.ToString());

            // Console.ReadLine();

        }



    }
    }


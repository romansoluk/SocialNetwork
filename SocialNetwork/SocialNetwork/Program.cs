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
using System.Web.UI.WebControls;
using Neo4jClient;

namespace SocialNetwork
{
    class Program
    {

     


static void Main(string[] args)
            {




            IGraphClient client = new GraphClient(new Uri("http://localhost:7474/db/data"), "1", "1");

           
            
                client.Connect();
            








            string con = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
                MongoClient client = new MongoClient(con);
                UserDAL user = new UserDAL(con);
                 PostDAL postDAL = new PostDAL(con);
            CommentDAL commentDAL = new CommentDAL(con);
            UserDTO u = new UserDTO
            {
                Username = "joe",
                Password = "1234",
                Followers = new List<string>(),
                Following = new List<string>(),
                Posts= new List<PostDTO>()

            };

            UserDTO u1 = new UserDTO
            {
                Username = "liam",
                Password = "4321",
                Followers = new List<string>(),
                Following = new List<string>(),
                Posts = new List<PostDTO>()



            };

            UserDTO u2 = new UserDTO
            {
                Username = "julie",
                Password = "4567",
                Followers = new List<string>(),
                Following = new List<string>(),
                Posts = new List<PostDTO>()

            };

            // user.CreateUser(u);
            //user.CreateUser(u1);
            //user.CreateUser(u2);
            //user.UpdateUserPassword(u);
            //user.FollowUser(u, u1);
            //user.FollowUser(u, u2);

            //    //GetDatabaseNames(client).GetAwaiter();
            //    Console.ReadLine();

            // Console.WriteLine(user.LoginUser("joe", "1234"));


            PostDTO post1 = new PostDTO
            {
                PostID = u.Username+" "+DateTime.Now.ToString(),
                UserID = u.Username,
                Text = "And this is my first post?!",
                Date = DateTime.Now,
                Like = new List<LikeDTO>(),
                Comments = new List<CommentDTO>()
            };
           //Console.WriteLine( post1.ToBsonDocument().GetElement(0));
            


           //postDAL.createPost(post1, u.Username);
            Console.WriteLine(post1.PostID);

            CommentDTO c1 = new CommentDTO
            {
                PostID = post1.PostID.ToString(),
                UserID = u2.Username,
                Text = "Hello here"

            };

            //commentDAL.createComment(c1, post1.PostID.ToString());

            Console.ReadLine();

            }

            private static async Task GetDatabaseNames(MongoClient client)
            {
                using (var cursor = await client.ListDatabasesAsync())
                {
                    var databaseDocuments = await cursor.ToListAsync();
                    foreach (var databaseDocument in databaseDocuments)
                    {
                        Console.WriteLine(databaseDocument["name"]);
                    }
                }
            }
        }
    }


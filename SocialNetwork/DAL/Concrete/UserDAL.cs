using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace DAL.Concrete
{
    public class UserDAL : IUserDAL
    {

        private string _connectionString;
        IMongoCollection<UserDTO> Users;
        

        public UserDAL(string connectionString)
        {
            _connectionString = connectionString;
            var connection = new MongoUrlBuilder(connectionString);
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(connection.DatabaseName);
            IGridFSBucket gridFS = new GridFSBucket(database);
            Users = database.GetCollection<UserDTO>("User");
        }

        public void CreateUser(UserDTO user)
        {
          Users.InsertOne(user);
        }


        public void UpdateUserPassword(UserDTO user)
        {
            Users.UpdateOne(Builders<UserDTO>.Filter.Eq("_id", user.Username), Builders<UserDTO>.Update.Set("Password", user.Password));
        }


        public void UpdateUserDescription(UserDTO user)
        {

            Users.UpdateOne(Builders<UserDTO>.Filter.Eq("_id", user.Username), Builders<UserDTO>.Update.Set("Description", user.Description));
          
   
        }


        public bool LoginUser(string username, string password)
        {
            try
            {


                username = Users.Find(Builders<UserDTO>.Filter.Eq("_id", username)).First().Username;
                if (Users.Find(Builders<UserDTO>.Filter.Eq("_id", username)).First().Password == password)
                {
                    return true;
                }
                else return false;
            }
            catch
            {
                return false;
            }
           
           
        }


        public void FollowUser(UserDTO user, UserDTO userToFollow)
        {
            user.Following.Add(userToFollow.Username);
            Users.UpdateOne(Builders<UserDTO>.Filter.Eq("_id", user.Username), Builders<UserDTO>.Update.Set("Following", user.Following));
            userToFollow.Followers.Add(user.Username);
            Users.UpdateOne(Builders<UserDTO>.Filter.Eq("_id", userToFollow.Username), Builders<UserDTO>.Update.Set("Followers", userToFollow.Followers));
        }

        public void unfollowUser(UserDTO user, UserDTO userToFollow)
        {
            user.Following.Remove(userToFollow.Username);
            Users.UpdateOne(Builders<UserDTO>.Filter.Eq("_id", user.Username), Builders<UserDTO>.Update.Set("Following", user.Following));
            userToFollow.Followers.Remove(user.Username);
            Users.UpdateOne(Builders<UserDTO>.Filter.Eq("_id", userToFollow.Username), Builders<UserDTO>.Update.Set("Followers", userToFollow.Followers));
        }

        public UserDTO findUser(string username)
        {
            return Users.Find(Builders<UserDTO>.Filter.Eq("_id", username)).First();
        }

        public void deleteUser(UserDTO user)
        {
            Users.DeleteOne(Builders<UserDTO>.Filter.Eq("_id", user.Username));
        }

        public List<UserDTO> getUsers()
        {
            return Users.Find(Builders<UserDTO>.Filter.AnyNe("_id", 0)).ToList();
        }



    }
}

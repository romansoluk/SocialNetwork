using DALNeo4j.Interfaces;
using DTONeo4j;
using Neo4jClient;
using System;
using Neo4j;
using Neo4jClient.Cypher; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALNeo4j.Concrete
{
    public class UserDALNeo4j : IUserDALNeo4j
    {
        private readonly string _connectionString;
        private readonly string _login;
        private readonly string _password;
        public UserDALNeo4j(string connectionString, string login, string password)
        {
            _connectionString = connectionString;
            _login = login;
            _password = password;
        }

        //relationship                       
        //follower: user1 follow user2, but is not followed back by user2          
       
        //relation creates when user1 starts following user2
        public void CreateRelationship(UserDTONeo4j user1, UserDTONeo4j user2)
        {
            using (var client = new GraphClient(new Uri(_connectionString), _login, _password))

            {
                client.Connect();
                client.Cypher
                      .Match("(user1:User),(user2:User)")
                      .Where("user1.User_Id = {username1}")
                      .AndWhere("user2.User_Id = {username2}")
                      .WithParam("username1", user1.Username)
                      .WithParam("username2", user2.Username)
                      .Create("(user1)-[:Follower]->(user2)")
                      .ExecuteWithoutResults();
            }
        }


        public void CreateUser(UserDTONeo4j user)
        {
            using (var client = new GraphClient(new Uri(_connectionString), _login, _password))
            {
                client.Connect();

                client.Cypher.Create("(user:User { Username: {username}, Following: {following}, Followers: {followers}, })")
                    .WithParam("username", user.Username)
                    .WithParam("following", user.Following)
                    .WithParam("followers", user.Followers)
                    .ExecuteWithoutResults();
            }
        }

        //checks whether user1 follows user2 or not
        public bool IsFollowing(UserDTONeo4j user1, UserDTONeo4j user2)
        {
            using (var client = new GraphClient(new Uri(_connectionString), _login, _password))
            {
                client.Connect();
                var follower = client.Cypher
                   .Match("(user1:User)-[r:Follower]-(user2:User)")
                   .Where((UserDTONeo4j User1) => User1.Username == user1.Username)
                   .AndWhere((UserDTONeo4j User2) => User2.Username == user2.Username)
                   .Return(r => r.As<Follower>()).Results; 
                if (follower.Count() == 1)
                {
                    return true;
                }
                return false;

            }
        }


        public int ShortestPath(string username1, string username2)
        {
            using (var client = new GraphClient(new Uri(_connectionString), _login, _password))
            {
                int path = 0;
                client.Connect();
                var res = client.Cypher
                    .Match("(username1:User{Username: {username1} }),(username2:User{Username: {username2} })," +
                    " p = shortestPath((username1)-[:Follower*]-(username2))")
                    .WithParam("username1", username1)
                    .WithParam("username2", username2)
                    .Return(relations => relations.As<Result>())
                    .Results;
              
                foreach (var relation in res)
                {
                    path = Convert.ToInt32(relation.length);
                }
                return path;
            }
        }


        public void DeleteRelationship(UserDTONeo4j user1, UserDTONeo4j user2)
        {
            using (var client = new GraphClient(new Uri(_connectionString), _login, _password))
            {
                client.Connect();
                client.Cypher
                    .Match("(user1:User)-[r:Follower]-(user2:User)")
                    .Where("user1.Username = {username1}")
                    .AndWhere("user2.User_Id = {username2}")
                    .WithParam("username1", user1.Username)
                    .WithParam("username2", user2.Username)
                    .Delete("r")
                    .ExecuteWithoutResults();

            }
        }


        public void DeleteUser(UserDTONeo4j user)
        {
            using (var client = new GraphClient(new Uri(_connectionString), _login, _password))
            {
                client.Connect();
                client.Cypher
                    .Match("(user1:User)-[r:Follower]-()")
                    .Where("user1.Username = {username}")
                    .WithParam("username", user.Username)
                    .Delete("r,user1")
                    .ExecuteWithoutResults();

            }
        }


    }
}

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


        public void AddRelationship(UserDTONeo4j user1, UserDTONeo4j user2)
        {

            using (var client = new GraphClient(new Uri("http://localhost:7474/db/data)"), _login, _password))
            {
                client.Connect();
            }
           //using (var client = new GraphClient(new Uri(_connectionString), _login, _password))


            //{

           
            //    client.Connect();
            //    client.Cypher
            //        .Match("(user1:User),(user2:User)")
            //        .Where("user1.User_Id = {p_id1}")
            //        .AndWhere("user2.User_Id = {p_id2}")
            //        .WithParam("p_id1", u1.User_Id)
            //        .WithParam("p_id2", u2.User_Id)
            //        .Create("(user1)-[:Friends]->(user2)")
            //        .ExecuteWithoutResults();
            //}
        }

    }
}

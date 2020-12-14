using DTONeo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALNeo4j.Interfaces
{
    public interface IUserDALNeo4j
    {

        void CreateRelationship(UserDTONeo4j user1, UserDTONeo4j user2);
        void CreateUser(UserDTONeo4j user);
        bool IsFollowing(UserDTONeo4j user1, UserDTONeo4j user2);
        int ShortestPath(string username1, string username2);
        void DeleteRelationship(UserDTONeo4j user1, UserDTONeo4j user2);
        void DeleteUser(UserDTONeo4j user);

    }
}

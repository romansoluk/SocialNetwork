using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace DAL.Interfaces
{
    public interface IUserDAL
    {
        void CreateUser(UserDTO user);
        void UpdateUserPassword(UserDTO user);
        void UpdateUserDescription(UserDTO user);
        bool LoginUser(string username, string password);
        void FollowUser(UserDTO user, UserDTO userToFollow);
        void unfollowUser(UserDTO user, UserDTO userToFollow);
        UserDTO findUser(string username);
        void deleteUser(UserDTO user);
        List<UserDTO> getUsers();
       

    }
}

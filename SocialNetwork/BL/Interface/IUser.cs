﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Concrete;
using Cassandra;
using DTO;
using DTOCassandra;

namespace BL.Interface
{
   public  interface IUser
    {
        void AddUser(UserDTO user);
        void UpdateUserDescription(UserDTO user);
        void UpdateUserPassword(UserDTO user);
        List<UserDTO> ShowUsers();
        void DeleteUser(UserDTO user);
        bool Login(string username, string password);
        UserDTO FindUser(string username);
        void FollowUser(UserDTO user, UserDTO userToFollow);
        void UnfollowUser(UserDTO user, UserDTO userToFollow);
        void AddPost(PostDTO post, string username);
        void DeletePost(PostDTO post, string username);
        List<PostDTO> ShowPosts(string username);
        List<PostDTO> Newsfeed(string username);
        bool Like(PostDTO post, string username, LikeDTO like);
        bool Dislike(PostDTO post, string username, LikeDTO like);
        void AddComment(CommentDTO comment, string PostID);
        void DeleteComment(CommentDTO comment, string PostID);
        List<CommentDTO> ShowComments(string PostID);
        string RelationshipStatus(UserDTO user1, UserDTO user2);
        void SynchronizeStream(PostDTOCassandra post, ISession session);
        void SyncronizeNewPost(PostDTOCassandra post, string username);
        void SyncronizeExistingPost(PostDTOCassandra post, string username);
        void SyncronizeComment(PostDTOCassandra post, CommentDTOCassandra comment, string username);

    }
}

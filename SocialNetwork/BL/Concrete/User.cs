using BL.Interface;
using DAL.Concrete;
using DAL.Interfaces;
using DALNeo4j.Interfaces;
using DTO;
using DTONeo4j;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Concrete
{
    class User : IUser
    {


        private readonly IUserDAL _user;
        private readonly IPostDAL _post;
        private readonly ICommentDAL _comment;
        private readonly ILikeDAL _like;


        private readonly IUserDALNeo4j _userNeo4j; 
        public User(IUserDAL user, IPostDAL post, ICommentDAL comment, ILikeDAL like, IUserDALNeo4j userDALNeo4J)
        {
            _user = user;
            _post = post;
            _comment = comment;
            _like = like;
            _userNeo4j = userDALNeo4J;
        }
        


        
        public void AddUser(UserDTO user)
        {
            UserDTONeo4j neo4J = new 
            _user.CreateUser(user);
        }

        public void UpdateUserDescription(UserDTO user)
        {
            _user.UpdateUserDescription(user);
        }

        public void UpdateUserPassword(UserDTO user)
        {
            _user.UpdateUserPassword(user);
        }

        public List<UserDTO> ShowUsers()
        {
            return _user.getUsers();
        }

        public void DeleteUser(UserDTO user)
        {
            _user.deleteUser(user);
        }

        public bool Login(string username, string password)
        {
            return _user.LoginUser(username, password);
        }

        public UserDTO FindUser(string username)
        {
           return _user.findUser(username);
        }

        public void FollowUser(UserDTO user, UserDTO userToFollow)
        {
            _user.FollowUser(user, userToFollow);
        }

        public void UnfollowUser(UserDTO user, UserDTO userToFollow)
        {
            _user.unfollowUser(user, userToFollow);
            
        }

       public void AddPost(PostDTO post, string username)
        {
            _post.createPost(post, username);
        }

        public void DeletePost(PostDTO post, string username)
        {
            _post.deletePost(post, username);
        }

        public List<PostDTO> ShowPosts(string username)
        {
           return _post.showPosts(username);
        }

        public List<PostDTO> Newsfeed(string username)
        {
            return _post.Newsfeed(username);
        }

        public bool Like(PostDTO post, string username, LikeDTO like)
        {
            return _like.setLike(post, username, like);
        }

        public bool Dislike(PostDTO post, string username, LikeDTO like)
        {
            return _like.dislike(post, username, like);
        }

        public void AddComment(CommentDTO comment, string PostID)
        {
            _comment.createComment(comment, PostID);

        }

        public void DeleteComment(CommentDTO comment, string PostID)
        {
            _comment.deleteComment(comment, PostID);
        }


        public List<CommentDTO> ShowComments(string PostID)
        {
            return _comment.showComments(PostID);
        }



    }
}

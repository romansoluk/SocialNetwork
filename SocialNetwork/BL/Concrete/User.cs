using BL.Interface;
using Cassandra;
using DAL.Concrete;
using DAL.Interfaces;
using DALCassandra.Interfaces;
using DALNeo4j.Interfaces;
using DTO;
using DTOCassandra;
using DTONeo4j;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Concrete
{
    public class User : IUser
    {


        private readonly IUserDAL _user;
        private readonly IPostDAL _post;
        private readonly ICommentDAL _comment;
        private readonly ILikeDAL _like;
        

        private readonly IUserDALNeo4j _userNeo4j; 

        private readonly IStreamDALCassandra _stream;
        private readonly IStreamDataCassandra _streamData;

        public User(IUserDAL user, IPostDAL post, ICommentDAL comment, ILikeDAL like, IUserDALNeo4j userDALNeo4J, IStreamDALCassandra stream, IStreamDataCassandra streamData)
        {
            _user = user;
            _post = post;
            _comment = comment;
            _like = like;
            _userNeo4j = userDALNeo4J;
            _streamData = streamData;
            _stream = stream;
        }
        

        public void SynchronizeStream(PostDTOCassandra post, ISession session)
        {
          
            _stream.SynchronizeStream( session, post);
        }


        public void SyncronizeNewPost(PostDTOCassandra post, string username)
            {
            _streamData.CreatePost(post, username);
            }


        public void SyncronizeExistingPost(PostDTOCassandra post, string username)
        {
            _streamData.UpdatePost(post, username);
        }

        public void SyncronizeComment(PostDTOCassandra post, CommentDTOCassandra comment, string username)
        {
            _streamData.CreateComment(post, comment, username);
        }


        //neo4j
        public string RelationshipStatus(UserDTO user1, UserDTO user2 )
        {
            UserDTONeo4j neo4J = new UserDTONeo4j(user1.Username, user1.Followers, user1.Following);
            UserDTONeo4j neo4JToFollow = new UserDTONeo4j(user2.Username, user2.Followers, user2.Following);
            int length = _userNeo4j.ShortestPath(neo4J.Username, neo4JToFollow.Username);
            if(length==0)
            {
                return "You have no mutual friends with " + neo4JToFollow.Username;
            }
            if(length ==1)
            {
                return "You are following " + neo4JToFollow.Username;
            }
            if(length ==2)
            {
                return "You have mutual friends with " + neo4JToFollow.Username;
            }
            else
            {
                return "You may have some mutual friends with " + neo4JToFollow.Username;
            }
        }

        //neo4j
        public void AddUser(UserDTO user)
        {
            UserDTONeo4j neo4J = new UserDTONeo4j(user.Username, user.Followers, user.Following);
            _user.CreateUser(user);
            _userNeo4j.CreateUser(neo4J);
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

        //neo4j
        public void DeleteUser(UserDTO user)
        {
            UserDTONeo4j neo4J = new UserDTONeo4j(user.Username, user.Followers, user.Following);
            _user.deleteUser(user);
            _userNeo4j.DeleteUser(neo4J);
            
        }

        public bool Login(string username, string password)
        {
            return _user.LoginUser(username, password);
        }

        public UserDTO FindUser(string username)
        {
           return _user.findUser(username);
        }

        //neo4j
        public void FollowUser(UserDTO user, UserDTO userToFollow)
        {
            UserDTONeo4j neo4J = new UserDTONeo4j(user.Username, user.Followers, user.Following);
            UserDTONeo4j neo4JToFollow = new UserDTONeo4j(userToFollow.Username, userToFollow.Followers, userToFollow.Following);
            if (_userNeo4j.IsFollowing(neo4J, neo4JToFollow) == false)
            {
                _user.FollowUser(user, userToFollow);
                _userNeo4j.CreateRelationship(neo4J, neo4JToFollow);
            }
        }

        //neo4j
        public void UnfollowUser(UserDTO user, UserDTO userToFollow)
        {
            UserDTONeo4j neo4J = new UserDTONeo4j(user.Username, user.Followers, user.Following);
            UserDTONeo4j neo4JToFollow = new UserDTONeo4j(userToFollow.Username, userToFollow.Followers, userToFollow.Following);
            if (_userNeo4j.IsFollowing(neo4J, neo4JToFollow))
            {
                _user.unfollowUser(user, userToFollow);
                _userNeo4j.DeleteRelationship(neo4J, neo4JToFollow);
            }
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

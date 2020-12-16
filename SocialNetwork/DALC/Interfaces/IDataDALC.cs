using Cassandra;
using DTOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALC.Interfaces
{
    public interface IDataDALC
    {
        void CreatePost(ISession session, string Text, Guid UserID);
        void UpdatePost(ISession session, PostC post);
        void CreateComment(ISession session, PostC post, string Text, Guid UserID);
    }
}

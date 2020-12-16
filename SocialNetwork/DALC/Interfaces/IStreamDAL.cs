using Cassandra;
using DTOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALC.Interfaces
{
    public interface IStreamDAL
    {
        void SynchronizeStream(ISession session, PostC post);
    }
}

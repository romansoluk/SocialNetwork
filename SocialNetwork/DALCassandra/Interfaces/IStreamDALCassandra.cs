using Cassandra;
using DTOCassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALCassandra.Interfaces
{
   public interface IStreamDALCassandra
    {
        void SynchronizeStream(ISession session, PostDTOCassandra post);
    }
}

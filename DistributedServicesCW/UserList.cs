using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace DistributedServicesCW
{
    [CollectionDataContract(Name = "Users", Namespace="http://www.napier.ac.uk")]
    public class UserList : List<User>
    {

    }
}

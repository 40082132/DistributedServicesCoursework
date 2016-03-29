using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
namespace DistributedServicesCW
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        [WebGet(UriTemplate = "")]

        UserList GetUsers();

        [OperationContract]
        [WebGet(UriTemplate = "/{username}")]

        User GetUser(string username);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "")]

        User PostUser(User user);
    }
}

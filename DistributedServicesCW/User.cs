using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Runtime.Serialization;

namespace DistributedServicesCW
{
    [DataContract(Namespace="http://www.napier.ac.uk")]
    public class User
    {
        [DataMember(Name = "first_name")]
        public string FirstName;
        [DataMember(Name = "last_name")]
        public string LastName;
        [DataMember(Name = "Email_Address")]
        public string EmailAddress;
        [DataMember(Name = "Username")]
        public string Username;
        [DataMember(Name = "Password")]
        public string Password;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Security.Cryptography;

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
        [DataMember(Name = "User Number")]
        public string UserNumber;
        

        public string hashPassword(string password)
        {
            SHA256 sha = new SHA256CryptoServiceProvider();

            sha.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password + EmailAddress));

            byte[] result = sha.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for(int i = 0; i<result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

    }
   
}

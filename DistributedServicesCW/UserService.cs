using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Collections.Generic;


namespace DistributedServicesCW
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class UserService : IUserService
    {
        private Dictionary<string, User> users = new Dictionary<string, User>();
        private int index = 1;
        public UserService()
        {
            User user = new User();
            user.EmailAddress = "Liamjbell09@aol.com";
            user.FirstName = "Liam";
            user.LastName = "Bell";
            user.Password = "password";
            user.Username = "LiamBell";
            user.UserNumber = "1";
            users.Add(user.UserNumber, user);
        }
        public UserList GetUsers()
        {
            UserList list = new UserList();
            list.AddRange(users.Values);
            return list;
        }
        public User GetUser(string usernumber)
        {
            return users[usernumber];
        }
        public User PostUser(User user)
        {
            string usernumber = "0" + index;
            while(users.ContainsKey(usernumber))
            {
                index++;
                usernumber = "0" + index;
            }
            user.UserNumber = usernumber;
            users.Add(usernumber, user);
            return user;
        }
        
    }
}

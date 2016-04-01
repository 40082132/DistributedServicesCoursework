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
        
        public bool emailisValid()
        {
           
            if (Regex.IsMatch(EmailAddress, @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool firstNameIsValid()
        {
            if (Regex.IsMatch(FirstName, @"/^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/u"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool lastNameIsValid()
        {
            if(Regex.IsMatch(LastName, @"/^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$/u"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool usernameIsValid()
        {
            if(Regex.IsMatch(Username, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*(_|[^\w])).+$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool passwordIsStrong()
        {
            if(Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*(_|[^\w])).+$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
   
}

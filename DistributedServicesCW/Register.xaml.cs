using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;

namespace DistributedServicesCW
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        string badpw;
        int count = 0;
        UserList users = new UserList();
        public List<string> unsafePasswordList = new List<string>();
        System.IO.StreamReader file = new System.IO.StreamReader("worstpasswords.txt");


        public Register()
        {

            InitializeComponent();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e, LoginDBDataSet database)
        {
            
            bool uniqueusername = true;
            User u1 = new User();
            while ((badpw = file.ReadLine()) != null)
            {
                unsafePasswordList.Add(badpw);
            }
            file.Close();
            
            passwordIsSafe();
            foreach (DataRow row in database.Login.Rows)
            {
                if (row.ItemArray[0].Equals(u1.Username))
                {
                    MessageBox.Show("Username already exists");
                    uniqueusername = false;
                }
            }
            if (u1.emailisValid() && u1.firstNameIsValid() && u1.lastNameIsValid() && u1.passwordIsStrong() && u1.usernameIsValid() && passwordIsSafe() && uniqueusername == true)
            {
                string encryptedPassword = u1.hashPassword(u1.Password);
                L

                FileStorageInterface filestore = new FileStorageInterface();
                filestore.Show();
                this.Close();
            }
            
        }
        
            
         
            
            
           
  
        public bool passwordIsSafe()
        {
            foreach (var p in unsafePasswordList)
            {
                if (txtPassword.Text == p)
                {
                    return false;
                   
                }

            }
            return true;
            }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {

        }
        }
        
    }

    

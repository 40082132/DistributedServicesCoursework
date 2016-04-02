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

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            User u1 = new User();
            while ((badpw = file.ReadLine()) != null)
            {
                unsafePasswordList.Add(badpw);
            }
            file.Close();
            
            passwordIsSafe();

            
                users.Add(u1);
                FileStorageInterface filestore = new FileStorageInterface();
                filestore.Show();
                this.Close();
            
        }
        private void AddUser(string username, string password, string first_name, string last_name, string email)
        {
            string smtpEmail = txtEmail.Text;
            string smtpPassword = txtPassword.Text;
            int smtpPort = (int)smtpPortNumericUD.value;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

            foreach(DataRow row in LoginServiceDataSet)
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
        }
        
    }

    

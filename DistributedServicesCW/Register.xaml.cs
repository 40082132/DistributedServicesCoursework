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

        SqlConnection connect = new SqlConnection();

        UserList users = new UserList();
        public List<string> unsafePasswordList = new List<string>();



        public Register()
        {

            InitializeComponent();
            connect.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|\\LoginData.mdf;Integrated Security=True";
            connect.Open();
            string s = connect.Database.ToString();
            MessageBox.Show(s);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
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

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            bool uniqueusername = true;
            User u1 = new User();


            SqlCommand cmd2 = new SqlCommand("select count(*) from [Logins] where Username = @Username", connect);
            cmd2.Parameters.AddWithValue("username", txtUsername.Text);
            uniqueusername = (int)cmd2.ExecuteScalar() > 0;
            if (uniqueusername.Equals(false))
            {
                MessageBox.Show("Username exists");
            }

            passwordIsSafe();


            if (u1.emailisValid() && u1.firstNameIsValid() && u1.lastNameIsValid() && u1.passwordIsStrong() && u1.usernameIsValid() && passwordIsSafe() && uniqueusername == true)
            {
                string encryptedPassword = u1.hashPassword(u1.Password);
                u1.Password = encryptedPassword;
                using (SqlCommand cmd3 = new SqlCommand("INSERT INTO [Logins] values (@Username, @Password, @Email, @First_Name, @Last_Name)", connect))
                {
                    cmd3.Parameters.AddWithValue("Username", txtUsername.Text);
                    cmd3.Parameters.AddWithValue("Password", txtPassword.Text);
                    cmd3.Parameters.AddWithValue("Email", txtEmail.Text);
                    cmd3.Parameters.AddWithValue("First_Name", txtFirstName.Text);
                    cmd3.Parameters.AddWithValue("Last_Name", txtLastName);

                    cmd3.ExecuteNonQuery();
                }
                connect.Close();


                FileStorageInterface filestore = new FileStorageInterface();
                filestore.Show();
                this.Close();
            }
        }
    }
}

    

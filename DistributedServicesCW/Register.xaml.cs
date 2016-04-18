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
       
        


        public Register()
        {

            InitializeComponent();
            connect.ConnectionString = "Server=tcp:distributedservices.database.windows.net,1433;Database=LoginDatabase;User ID=Liam@distributedservices;Password=Scotland1!;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            


    }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }


        public bool passwordIsSafe()
        {
            string[] logFile = System.IO.File.ReadAllLines(@"C:\Users\Liam\Documents\GitHub\DistributedServicesCoursework\worstpasswords.txt");
            List<string> unsafePasswordList = new List<string>();
            unsafePasswordList = logFile.ToList();
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
            

            
            bool passwordStrong = false;
            User u1 = new User();
            u1.FirstName = txtFirstName.Text;
            u1.LastName = txtLastName.Text;
            u1.Password = txtPassword.Text;
            u1.EmailAddress = txtEmail.Text;
            u1.Username = txtUsername.Text;
           

            passwordIsSafe();


            SqlDataAdapter sda = new SqlDataAdapter("SELECT Username FROM [Users]", connect);
            DataTable dt = new DataTable();
            sda.Fill(dt);


            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            
            Match match = regex.Match(u1.EmailAddress);

            if(passwordIsSafe() == false)
            {
                MessageBox.Show("Password is too common");
            }
            if(!match.Success)
            {
                MessageBox.Show("Email is invalid");
            }

            PasswordScore passwordStrength = PasswordAdvisor.CheckStrength(u1.Password);
            switch(passwordStrength)
            {
                case PasswordScore.Blank:
                case PasswordScore.VeryWeak:
                case PasswordScore.Weak:
                 
                case PasswordScore.Medium:
                case PasswordScore.Strong:
                case PasswordScore.VeryStrong:
                    passwordStrong = true;
                    break;
            }
                if (match.Success && u1.FirstName != "" && u1.LastName != "" && passwordIsSafe() == true)
                {
                    string cmdString = "INSERT INTO [Users] (Username, Password, Email, First_Name, Last_Name) VALUES (@val1, @val2, @val3, @val4, @val5)";


                    using (SqlCommand comm = new SqlCommand(cmdString, connect))
                    {
                        
                        comm.Parameters.AddWithValue("@val1", u1.Username);
                        comm.Parameters.AddWithValue("@val2", u1.Password);
                        comm.Parameters.AddWithValue("@val3", u1.EmailAddress);
                        comm.Parameters.AddWithValue("@val4", u1.FirstName);
                        comm.Parameters.AddWithValue("@val5", u1.LastName);
                        try
                        {
                        connect.Open();
                            comm.ExecuteNonQuery();
                        connect.Close();
                        }
                        catch (SqlException a)
                        {
                            MessageBox.Show(a.Message);
                        }
                    }


                    FileStorageInterface filestore = new FileStorageInterface(u1);
                    filestore.Show();
                    this.Close();
                
                }
            
            else
            {
                MessageBox.Show("Input is invalid");
            }
        }
        

       
    }

    
    }


    

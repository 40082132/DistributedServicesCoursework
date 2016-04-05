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
            connect.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|\\LoginData.mdf;Integrated Security=True";
            connect.Open();

            bool uniqueusername = true;
            User u1 = new User();
            u1.FirstName = txtFirstName.Text;
            u1.LastName = txtLastName.Text;
            u1.Password = txtPassword.Text;
            u1.EmailAddress = txtEmail.Text;
            u1.Username = txtUsername.Text;

            

            passwordIsSafe();

          
                SqlDataAdapter sda = new SqlDataAdapter("SELECT Username FROM [Logins]", connect);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                foreach (DataRow d in dt.Rows)
                {
                    if (d.ItemArray[0].Equals(u1.Username)) ;
                    {
                        uniqueusername = false;
                    }
                }
                if (uniqueusername == false)
                {
                    string cmdString = "INSERT INTO Logins (Username, Password, Email, First_Name, Last_Name) VALUES (@val1, @val2, @val3, @val4, @val5)";


                    using (SqlCommand comm = new SqlCommand())
                    {
                        comm.Connection = connect;
                        comm.CommandText = cmdString;
                        comm.Parameters.AddWithValue("@val1", txtUsername.Text);
                        comm.Parameters.AddWithValue("@val2", txtPassword.Text);
                        comm.Parameters.AddWithValue("@val3", txtEmail.Text);
                        comm.Parameters.AddWithValue("@val4", txtFirstName.Text);
                        comm.Parameters.AddWithValue("@val5", txtLastName.Text);
                        try
                        {
                            
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


    

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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace DistributedServicesCW
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
           
            InitializeComponent();
        }

        private void lblBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connect = new SqlConnection();
            connect.ConnectionString = connect.ConnectionString = "Server=tcp:distributedservices.database.windows.net,1433;Database=LoginDatabase;User ID=Liam@distributedservices;Password=Scotland1!;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            connect.Open();
            User u1 = new User();
            u1.Username = txtUsername.Text;
            u1.Password = txtPassword.Text;
            SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM [Users] WHERE Username='" + txtUsername.Text + "' AND Password='" + txtPassword.Text + "'", connect);
            DataTable dt = new DataTable(); //this is creating a virtual table  
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                
                
                FileStorageInterface filestore = new FileStorageInterface(u1);
                filestore.Show();
                this.Close();
            }
            else
                MessageBox.Show("Invalid username or password");
           
        }
    }
}

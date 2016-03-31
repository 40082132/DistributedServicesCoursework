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

namespace DistributedServicesCW
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        string badpw;
        int count = 0;
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
            while((badpw = file.ReadLine()) != null)
            {
                unsafePasswordList.Add(badpw);
            }
            file.Close();
            u1.EmailAddress = txtEmail.Text;
            u1.FirstName = txtFirstName.Text;
            u1.LastName = txtLastName.Text;
            u1.Username = txtUsername.Text;
            u1.Password = txtPassword.Text;
            foreach(var p in unsafePasswordList)
            {
                if (u1.Password == p)
                {

                }
            }

            FileStorageInterface filestore = new FileStorageInterface();
            filestore.Show();
            this.Close();
        }
    }
}

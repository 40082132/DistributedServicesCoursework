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
    /// Interaction logic for FileStorageInterface.xaml
    /// </summary>
    public partial class FileStorageInterface : Window
    {
        public FileStorageInterface()
        {
            InitializeComponent();
        }

        private void btnManage_Click(object sender, RoutedEventArgs e)
        {
            ProfileManage manage = new ProfileManage();
            manage.Show();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }
    }
}

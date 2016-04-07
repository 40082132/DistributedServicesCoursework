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
using System.Drawing;
using System.IO;
using System.ComponentModel;
using sss;
using sss.config;
using sss.crypto.data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;


namespace DistributedServicesCW 
{
    /// <summary>
    /// Interaction logic for FileStorageInterface.xaml
    /// </summary>
    public partial class FileStorageInterface : Window , IShareService
    {
        public FileStorageInterface(User u1) 
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

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            int n = 5, t = 3;
            RandomSources random = RandomSources.SHA1;
            Encryptors encrypt = Encryptors.AES;
            Algorithms a = Algorithms.CSS;
            System.IO.StreamReader file = new System.IO.StreamReader(lstFiles.SelectedItem.ToString());
            string secret = file.ReadToEnd();
            byte[] bytes = ObjectToByteArray(secret);
            Facade f = new Facade(n, t, random, encrypt, a);
            Share[] shares = f.split(bytes);
            int counter = n - t;
            List<Share> list = shares.ToList();
            Random rand = new Random();
            for (int i = 0; i < counter; i++)
            {
                list.RemoveAt(rand.Next(list.Count));
            }
            shares = list.ToArray();
            Object o = ByteArrayToObject(f.join(shares));
            int c = 0;
            byte[] s = null;
            for (c = 0; c < shares.Length; c++)
            {
                s = shares[c].serialize();
            }
            int sharenumbers = s.Length;
            byte[] storedData = SaveData(s, sharenumbers, "myblob", "mycontainer");
            string GetData = RetrieveData("myblob", "mycontainer");
            

            

            
            
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if(result == true)
            {
                string filename = dlg.FileName;
                lstFiles.Items.Add(filename);
                
            }
        }
        public static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();

            }
        }

        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }
        public byte[] SaveData(byte[] serialized, int sharenumbers, string blobName, string containerName)
        {
            string storageConnectionString =
        "DefaultEndpointsProtocol=https;AccountName=distributedservicecs;" +
        "AccountKey=ZNvGKlBuIaNDiybax9vrZTyMM3Jz9BUgpVFcyyVhy5BjGp8UxX4OW/liK9o0wBnYTt6zrNqTmtZSwnPQbt612w==";
            int index = 0;
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(storageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve a reference to a container 
            CloudBlobContainer container =
                blobClient.GetContainerReference(containerName);
            // Create the container if it doesn't already exist
            container.CreateIfNotExists();
            // Retrieve reference to a blob named "myblob"
            CloudBlockBlob blob = container.GetBlockBlobReference(blobName);
            // Create or overwrite the blob with content
            blob.UploadFromByteArray(serialized, index, sharenumbers);
            return serialized;
        }

        public string RetrieveData(string containerName, string blobName)
        {
            string storageConnectionString =
        "DefaultEndpointsProtocol=https;AccountName={distributedservicecs};" +
        "AccountKey={ZNvGKlBuIaNDiybax9vrZTyMM3Jz9BUgpVFcyyVhy5BjGp8UxX4OW/liK9o0wBnYTt6zrNqTmtZSwnPQbt612w==}";
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(storageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve a reference to a container 
            CloudBlobContainer container =
                blobClient.GetContainerReference(containerName);
            // Create the container if it doesn't already exist
            container.CreateIfNotExists();
            // Retrieve reference to a blob named "myblob"
            CloudBlockBlob blob = container.GetBlockBlobReference(blobName);
            StreamReader reader = new StreamReader(blob.OpenRead());
            return reader.ReadToEnd();
        }
    }
}

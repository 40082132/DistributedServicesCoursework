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
            Encryptors encrypt = Encryptors.ChaCha20;
            Algorithms a = Algorithms.CSS;
            System.IO.StreamReader file = new System.IO.StreamReader(lstFiles.SelectedItem.ToString());
            string secret = file.ReadToEnd();
            byte[] bytes = ObjectToByteArray(secret);
            Facade f = new Facade(n, t, random, encrypt, a);
            Share[] shares = f.split(bytes);
            int c = 0;
            
            foreach (Share s in shares)
            {
                c++;
                byte[] serialized = s.serialize();
                byte[] storedData = SaveData(serialized, serialized.Length, "myblob" + c , "mycontainer");
            }
            
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            lstFiles.Items.Clear();
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
      

        public byte[] RetrieveData(string containerName, string blobName)
        {
            string storageConnectionString =
        "DefaultEndpointsProtocol=https;AccountName=distributedservicecs;" +
        "AccountKey=ZNvGKlBuIaNDiybax9vrZTyMM3Jz9BUgpVFcyyVhy5BjGp8UxX4OW/liK9o0wBnYTt6zrNqTmtZSwnPQbt612w==";
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(storageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve a reference to a container 
            CloudBlobContainer container =
                blobClient.GetContainerReference(containerName);
            // Create the container if it doesn't already exist
            container.CreateIfNotExists();
            container.SetPermissions(
    new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            // Retrieve reference to a blob named "myblob"
            CloudBlockBlob blob = container.GetBlockBlobReference(blobName);
            blob.FetchAttributes();
            long fileLength = blob.Properties.Length;
            byte[] fileContent = new byte[fileLength];
            for (int i = 0; i < fileLength; i++)
            {
                fileContent[i] = 0x20;
            }
            blob.DownloadToByteArray(fileContent, 0);
            return fileContent;
        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            string filepath = null;
            Microsoft.Win32.SaveFileDialog pathdlg = new Microsoft.Win32.SaveFileDialog();
            Nullable<bool> result = pathdlg.ShowDialog();
            if (result == true)
            {
                filepath = pathdlg.FileName;

            }
            int n = 5, t = 3;
            RandomSources random = RandomSources.SHA1;
            Encryptors encrypt = Encryptors.ChaCha20;
            Algorithms a = Algorithms.CSS;
            Facade f = new Facade(n, t, random, encrypt, a);
            Share[] shares = null;
            List<Share> shareList = new List<Share>();
            int k = 0;
            for (int j = 0; j<n; j++)
            {
                k++;
                byte[] downloadedData = RetrieveData("mycontainer", "myblob" + k);
                Share s = SerializableShare.deserialize(downloadedData);
                shareList.Add(s);
            }
            int counter = n - t;
            Random rand = new Random();
            for (int i = 0; i < counter; i++)
                shareList.RemoveAt(rand.Next(shareList.Count));
            shares = shareList.ToArray();
            object o = ByteArrayToObject(f.join(shares));
            File.WriteAllText(filepath,((string)o));
            



        }

        private void btnList_Click(object sender, RoutedEventArgs e)
        {
            lstFiles.Items.Clear();
            string storageConnectionString =
        "DefaultEndpointsProtocol=https;AccountName=distributedservicecs;" +
        "AccountKey=ZNvGKlBuIaNDiybax9vrZTyMM3Jz9BUgpVFcyyVhy5BjGp8UxX4OW/liK9o0wBnYTt6zrNqTmtZSwnPQbt612w==";
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(storageConnectionString);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

            // Loop over items within the container and output the length and URI.
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    lstFiles.Items.Add(blob.Name);


                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;

                    lstFiles.Items.Add(pageBlob.Uri);
                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;
                    lstFiles.Items.Add(directory.Uri);
                    
                }
            }
        }

        private void btnManage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            string storageConnectionString =
       "DefaultEndpointsProtocol=https;AccountName=distributedservicecs;" +
       "AccountKey=ZNvGKlBuIaNDiybax9vrZTyMM3Jz9BUgpVFcyyVhy5BjGp8UxX4OW/liK9o0wBnYTt6zrNqTmtZSwnPQbt612w==";
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(storageConnectionString);
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");
            // Retrieve reference to a blob named "myblob.txt".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(lstFiles.SelectedItem.ToString());
            // Delete the blob.
            blockBlob.Delete();

        }
    }
}

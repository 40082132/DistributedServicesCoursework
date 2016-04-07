using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace DistributedServicesCW
{
    
    public interface IShareService
    {
        [OperationContract]
        bool SaveData(byte[] serialized, string blobName, string containerName);

        [OperationContract]
        string RetrieveData(string containerName, string blobName);
    }
}

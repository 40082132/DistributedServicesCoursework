﻿using System;
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
        byte[] SaveData(byte[] serialized, int sharenumbers, string blobName, string containerName);

        [OperationContract]
        byte[] RetrieveData(string containerName, string blobName);
    }
}

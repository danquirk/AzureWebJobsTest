using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.Azure;

namespace Utilities
{
    public enum JobQueues
    {
        RawIncoming,
        ProcessedIncoming,
        Outgoing
    }
    
    public static class Utils
    {
        public static List<string> RepoList = new List<string>();
        public static CloudQueue GetQueue(JobQueues queueName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference(queueName.AsString());
            queue.CreateIfNotExists();
            return queue;
        }

        public static string AsString(this JobQueues job)
        {
            switch(job)
            {
                case JobQueues.RawIncoming:
                    return "raw-incoming";
                case JobQueues.ProcessedIncoming:
                    return "processed-incoming";
                case JobQueues.Outgoing:
                    return "outgoing";
                default:
                    throw new Exception("Unknown enum value");
            }
        }
    }
}

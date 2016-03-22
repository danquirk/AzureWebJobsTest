using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

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
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
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
                    return "RawIncoming";
                case JobQueues.ProcessedIncoming:
                    return "ProcesssedIncoming";
                case JobQueues.Outgoing:
                    return "Outgoing";
                default:
                    throw new Exception("Unknown enum value");
            }
        }
    }
}

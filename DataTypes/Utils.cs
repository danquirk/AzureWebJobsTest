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
    public static class JobQueues
    {
        public const string RawIncoming = "raw-incoming";
        public const string ProcessedIncoming = "processed-incoming";
        public const string Outgoing = "outgoing";

        public static void ValidateQueueName(string name)
        {
            if(name != RawIncoming && name != ProcessedIncoming && name != Outgoing)
            {
                throw new ArgumentException("Invalid queue name: {0}", name);
            }
        }
    }
    
    public static class Utils
    {
        private static List<string> repoList;

        public static List<string> RepoList
        {
            get
            {
                if(repoList == null)
                {
                    repoList = new List<string>() { "AAAA", "BBBB", "CCCC", "DDDD" };
                }
                return repoList;
            }
        }

        public static CloudQueue GetQueue(string queueName)
        {
            JobQueues.ValidateQueueName(queueName);
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference(queueName);
            queue.CreateIfNotExists();
            return queue;
        }
    }
}

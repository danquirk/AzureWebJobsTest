using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace WebJobTest
{
    public static class Functions
    {
        public static void ProcessMessage([QueueTrigger("jobqueue")] string jobDescription)
        {
            Console.WriteLine("ProcessMessageFired");
            var job = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTypes.JobDescription>(jobDescription);
            Console.WriteLine("Job: {0}, recipient: {1}, message: {2}", job.PullRequestURL, job.RequestingEmail, job.RequestTime);

            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=codeformatterstorage;AccountKey=QJOjlTcC/hWA+tDY4HNc3zEtIXl+2HNsDhQlJbfPOVWNJQv4+DgK3oGq7JVe7vbCDExcezA3m239aIAzhIq72Q==;BlobEndpoint=https://codeformatterstorage.blob.core.windows.net/;TableEndpoint=https://codeformatterstorage.table.core.windows.net/;QueueEndpoint=https://codeformatterstorage.queue.core.windows.net/;FileEndpoint=https://codeformatterstorage.file.core.windows.net/";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting(connectionString));

            // Create the queue client.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue.
            CloudQueue queue = queueClient.GetQueueReference("jobqueue");

            // Create the queue if it doesn't already exist.
            queue.CreateIfNotExists();

            var nextMessage = queue.PeekMessage();
            if (nextMessage != null)
            {
                Console.WriteLine("Next message: {0}", nextMessage.AsString);
            }
        }
    }
}

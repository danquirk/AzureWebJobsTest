using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace WebQueueTest.Controllers
{
    public class JobController : Controller
    {
        // GET: Job
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PostJob()
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=codeformatterstorage;AccountKey=QJOjlTcC/hWA+tDY4HNc3zEtIXl+2HNsDhQlJbfPOVWNJQv4+DgK3oGq7JVe7vbCDExcezA3m239aIAzhIq72Q==;BlobEndpoint=https://codeformatterstorage.blob.core.windows.net/;TableEndpoint=https://codeformatterstorage.table.core.windows.net/;QueueEndpoint=https://codeformatterstorage.queue.core.windows.net/;FileEndpoint=https://codeformatterstorage.file.core.windows.net/";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting(connectionString));

            // Create the queue client.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue.
            CloudQueue queue = queueClient.GetQueueReference("jobqueue");

            // Create the queue if it doesn't already exist.
            queue.CreateIfNotExists();

            // Create a message and add it to the queue.
            var job = new DataTypes.JobDescription() {  PullRequestURL = "http://github.com/microsoft/typescript/123124", RequestingEmail = "danquirk@adf.asdf", RequestTime = DateTime.Now };
            CloudQueueMessage queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(job));
            queue.AddMessage(queueMessage);

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using Utilities;

namespace WebQueueTest.Controllers
{
    public class JobController : Controller
    {
        // GET: Job
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PostJob(string pullRequestURL, string email)
        {
            var queue = Utils.GetQueue();

            // Create a message and add it to the queue.
            var job = new JobDescription() {
                PullRequestURL = pullRequestURL ?? "http://www.github.com/microsoft/typescript/00000",
                RequestingEmail = email ?? "email@email.com",
                RequestTime = DateTime.Now
            };
                
            CloudQueueMessage queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(job));
            queue.AddMessage(queueMessage);

            return View();
        }
    }
}
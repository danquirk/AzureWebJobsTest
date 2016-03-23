using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Utilities;

namespace WebJobTest
{
    public static class Functions
    {
        public static void ProcessMessage([QueueTrigger("jobqueue")] string jobDescription)
        {
            Console.WriteLine("ProcessMessageFired");
            var job = Newtonsoft.Json.JsonConvert.DeserializeObject<JobDescription>(jobDescription);
            Console.WriteLine("Job: {0}, recipient: {1}, message: {2}", job.PullRequestURL, job.RequestingEmail, job.RequestTime);

            var queue = Utils.GetQueue(JobQueues.ProcessedIncoming);
            foreach (var repo in Utils.RepoList)
            {
                //queue.AddMessage();
            }            
        }
    }
}
